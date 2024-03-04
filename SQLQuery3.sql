--Select * From UserAcc
--Where UPPER(REPLACE(userFName+userLName, ' ','')) = UPPER(REPLACE('suzybae',' ','')) Or (ISNUMERIC('suzybae') = 1 and userID = CAST('suzybae' As Int))

Select * From UserAcc
Where UPPER(REPLACE(userFName+userLName, ' ','')) = UPPER(REPLACE('suzybae',' ','')) 
Or (ISNUMERIC('suzybae') = 1 and userID = CONVERT(Int, 'suzybae'))