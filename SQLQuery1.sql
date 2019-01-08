SELECT ISNULL(state_repair, 0) state_repair FROM good WHERE good_sys_id = 27521
SELECT * FROM cash_history WHERE good_sys_id = 27521 ORDER BY sys_id DESC

SELECT * FROM sms_send ss WHERE ss.sms_type_sys_id = 6 AND good_sys_id = 27521
SELECT  * FROM sms_type

SELECT * FROM cash_history WHERE state = 5 AND good_sys_id= 1211 AND sys_id > 1232


SELECT TOP 1 * FROM 
	(SELECT DISTINCT
	g.*,
	(SELECT COUNT(*) FROM sms_send ss 
	WHERE ss.good_sys_id=g.good_sys_id 
	AND ss.sms_type_sys_id = 4 
	AND ss.update_date > g.skno_received_update_date) AS count_sms_with_type_4,
	(SELECT TOP 1 owner_sys_id 
	FROM cash_history
	WHERE good_sys_id=g.good_sys_id
	ORDER BY sys_id DESC) AS owner_sys_id
	FROM good g
	WHERE g.state_repair = 10 
	AND g.skno_received = 1 
	AND g.skno_received_update_date IS NOT NULL) AS glt
	INNER JOIN customer_tel_notice ctn ON glt.owner_sys_id = ctn.customer_sys_id
	WHERE ctn.customer_tel_notice_type IN (1,2)
	ORDER BY ctn.customer_tel_notice_type DESC, ctn.update_date DESC