SELECT ISNULL(state_repair, 0) state_repair FROM good WHERE good_sys_id = 27521
SELECT * FROM cash_history WHERE good_sys_id = 27521 ORDER BY sys_id DESC