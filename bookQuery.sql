SELECT
                                bd.BOOK_ID,
	                            bd.BOOK_NAME,
	                            bd.BOOK_AUTHOR,
	                            bd.BOOK_PUBLISHER,
	                            bd.BOOK_NOTE,
	                            bd.BOOK_BOUGHT_DATE AS BOOK_BOUGHT_DATE,
	                            bc.BOOK_CLASS_NAME,
	                            bc1.CODE_NAME,
	                            mm.USER_ENAME
                            FROM BOOK_DATA AS bd
                            LEFT JOIN BOOK_CLASS AS bc
	                            ON bd.BOOK_CLASS_ID = bc.BOOK_CLASS_ID
                            LEFT JOIN BOOK_CODE AS bc1
	                            ON bd.BOOK_STATUS = bc1.CODE_ID
		                            AND bc1.CODE_TYPE_DESC = '®ÑÄyª¬ºA'
                            LEFT JOIN MEMBER_M AS mm
	                            ON bd.BOOK_KEEPER = mm.USER_ID
                            WHERE BOOK_ID='213'



							Select M.USER_ID,M.USER_ENAME
                           From DBO.MEMBER_M AS M
						   LEFT JOIN BOOK_DATA AS bd
						   ON M.USER_ID= bd.BOOK_KEEPER
                            WHERE BOOK_ID=1

							SELECT bc.CODE_ID,bc.CODE_NAME 
							FROM BOOK_CODE AS bc
							LEFT JOIN BOOK_DATA AS bd
							ON bc.CODE_ID=bd.BOOK_STATUS
							WHERE CODE_TYPE='BOOK_STATUS' AND BOOK_ID=1 

							Select bcl.BOOK_CLASS_ID, bcl.BOOK_CLASS_NAME 
                           From BOOK_CLASS AS bcl
						   LEFT JOIN BOOK_DATA AS bd
						   ON bcl.BOOK_CLASS_ID = bd.BOOK_CLASS_ID
                            WHERE BOOK_ID=1





						   	SELECT CODE_ID,CODE_NAME 
							FROM BOOK_CODE							
							WHERE CODE_TYPE='BOOK_STATUS' 



							select  bcl.BOOK_CLASS_ID, bcl.BOOK_CLASS_NAME 
                           From BOOK_CLASS AS bcl
						   LEFT JOIN BOOK_DATA AS bd
						   ON bcl.BOOK_CLASS_ID = bd.BOOK_CLASS_ID
						   WHERE bd.BOOK_ID=1

						   Delete FROM BOOK_DATA Where BOOK_ID=4
						   SELECT * FROM BOOK_DATA Where BOOK_ID=1582
						    SELECT * FROM BOOK_DATA ORDER BY CREATE_DATE DESC



 WITH CTE
 AS
 (SELECT
		 BOOK_NAME
		,BOOK_AUTHOR
		,BOOK_PUBLISHER
		,BOOK_NOTE
		,BOOK_BOUGHT_DATE
		,BOOK_CLASS_ID
		,BOOK_STATUS
		,BOOK_KEEPER
		,MODIFY_DATE
                                        ,MODIFY_USER     
	 FROM BOOK_DATA
	 WHERE BOOK_ID = 1)

 UPDATE CTE
 SET BOOK_NAME = 'Microsoft SQL Server Documentation'
	,BOOK_AUTHOR = ''
	,BOOK_PUBLISHER = ''
	,BOOK_NOTE = ''
	,BOOK_BOUGHT_DATE = ''
	,BOOK_CLASS_ID = 'DB'
	,BOOK_STATUS = 'A'
	,MODIFY_DATE = GETDATE()
                                    ,MODIFY_USER='180'
	,BOOK_KEEPER =
	 CASE
		 WHEN BOOK_STATUS = 'A' OR
			 BOOK_STATUS = 'D' THEN 'hi'
		 ELSE 'pp'
	 END


SELECT * FROM BOOK_DATA bd WHERE bd.BOOK_ID=1

INSERT INTO BOOK_DATA (BOOK_NAME, BOOK_AUTHOR, BOOK_PUBLISHER, BOOK_NOTE, BOOK_BOUGHT_DATE, BOOK_CLASS_ID,BOOK_STATUS,BOOK_KEEPER)
	VALUES ('a', 'b', 'c', 'd', '2000-12-12', 'DB','A',NULL)


	SELECT                                bd.BOOK_ID,
	                            bd.BOOK_NAME,
	                            bd.BOOK_AUTHOR,
	                            bd.BOOK_PUBLISHER,
	                            bd.BOOK_NOTE,
	                            bd.BOOK_BOUGHT_DATE AS BOOK_BOUGHT_DATE,
	                            bc.BOOK_CLASS_ID,
                                bd.BOOK_STATUS ,	                            
	                            bd.BOOK_KEEPER
                            FROM BOOK_DATA AS bd
                            LEFT JOIN BOOK_CLASS AS bc
	                            ON bd.BOOK_CLASS_ID = bc.BOOK_CLASS_ID
                            LEFT JOIN BOOK_CODE AS bc1
	                            ON bd.BOOK_STATUS = bc1.CODE_ID
		                            AND bc1.CODE_TYPE_DESC = '®ÑÄyª¬ºA'
                            LEFT JOIN MEMBER_M AS mm
	                            ON bd.BOOK_KEEPER = mm.USER_ID
                            WHERE BOOK_ID=1

							SELECT * FROM BOOK_LEND_RECORD ORDER BY BOOK_ID DESC
							SELECT LEND_DATE,KEEPER_ID,blr.KEEPER_ID,m.USER_CNAME 
							FROM BOOK_LEND_RECORD AS blr LEFT JOIN MEMBER_M AS m ON blr.KEEPER_ID=m.USER_ID 
							WHERE BOOK_ID=1582


							SELECT blr.LEND_DATE AS LEND_DATE,
                                  blr.KEEPER_ID AS KEEPER_ID,
								  m.USER_ENAME AS USER_ENAME,
                                  m.USER_CNAME AS USER_CNAME
                                  FROM BOOK_LEND_RECORD AS blr 
                                  LEFT JOIN MEMBER_M AS m 
                                  ON blr.KEEPER_ID=m.USER_ID 
                                  WHERE BOOK_ID=1582

















