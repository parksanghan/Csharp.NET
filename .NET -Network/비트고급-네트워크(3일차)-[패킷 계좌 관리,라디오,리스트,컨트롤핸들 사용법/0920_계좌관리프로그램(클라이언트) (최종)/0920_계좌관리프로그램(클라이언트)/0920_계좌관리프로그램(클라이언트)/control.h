//control.h
#pragma once

void con_init();
void con_exit();

void con_account_insert();
void con_account_select();
void con_account_input();
void con_account_output();
void con_account_delete();
void con_account_printall();

int accid_to_idx(int accid);
