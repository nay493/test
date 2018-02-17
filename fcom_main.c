#include <stdio.h>
#include <pthread.h>
#include <string.h>
#include <stdbool.h>

void process_the_secure_file(char *fname, char opt);
void the_file_comp_help(void);
void close_the_file_comp(void);

struct file_ops_status {
	char fname[100];
	bool process_complete;
};

static struct file_ops_status store_file_op_status;
static struct file_ops_status retrive_file_op_status;

pthread_t file_encrypt_comp_td;
pthread_t file_decrypt_decomp_td;

pthread_mutex_t *store_mutex;
pthread_mutex_t *retrive_mutex;

int main (void)
{
	char opt;
	char fname[100];

	pthread_mutex_init(store_mutex, NULL);
	pthread_mutex_init(retrive_mutex, NULL);
	
	while (1) {
		printf("Enter the option:");
		scanf("%c", &opt);
		fflush(stdin);
		switch (opt) {
		case 'e':
		case 'r':
			printf("Enter the file name");
			scanf("%s", fname);
			process_the_secure_file(fname, opt);
			break;
		case 'h':
			the_file_comp_help();
			break;
		case 'x':
			close_the_file_comp();
			goto close;
			break;
		default:
			printf("invalid option....");
		}
	}

close:
	return 0;
}

void store_file_ops_status(char *ifname, bool status)
{
	strcpy(store_file_op_status.fname, ifname);
	store_file_op_status.process_complete = status;
}

void retrive_file_ops_status(char *ifname, bool status)
{
	strcpy(retrive_file_op_status.fname, ifname);
	retrive_file_op_status.process_complete = status;
}

void *store_the_secure_file(void *fname)
{
	char *ifname;
	
	pthread_mutex_lock(store_mutex);
	ifname = (char *)fname;
	store_file_ops_status(ifname, false);
	/* encrpt_comp */
	store_file_ops_status(ifname, true);
	pthread_mutex_unlock(store_mutex);
}

void *retrive_the_secure_file(void *fname)
{
	char *ifname;
	pthread_mutex_lock(retrive_mutex);
	ifname = (char *)fname;
	retrive_file_ops_status(ifname, false);
	/* decomp_decrypt */
	retrive_file_ops_status(ifname, true);
	pthread_mutex_unlock(retrive_mutex);
}

void process_the_secure_file(char *fname, char opt)
{
    int ret;
    
	if (opt == 'e')
		ret = pthread_create(&file_encrypt_comp_td, NULL, store_the_secure_file, fname);
	
	if (opt == 'r')
		ret = pthread_create(&file_decrypt_decomp_td, NULL, retrive_the_secure_file, fname);
}

void the_file_comp_help(void)
{
	printf("file_comp e filename.txt\n");
	printf("file_comp r filename\n");
	printf("file_comp h for supported features\n");
	printf("file_comp x close the file_comp\n");
}

void close_the_file_comp(void)
{
	printf("closing the file comp");
	pthread_join(file_encrypt_comp_td, NULL);
	pthread_join(file_decrypt_decomp_td, NULL);
	pthread_mutex_destroy(store_mutex);
	pthread_mutex_destroy(retrive_mutex);
	/*
	 * wait for complte the pending task
	 * kill the file process thread
	 * relase the requested memory
	 * close all opened file.
	 */
}

