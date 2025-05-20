USE MTS;
GO
EXEC GetMostActiveClients @start_date = '2024-01-01', @end_date = '2024-08-01', @top_n = 5;
