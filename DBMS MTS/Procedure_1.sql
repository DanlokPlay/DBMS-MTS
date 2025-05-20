USE MTS;
GO

IF OBJECT_ID('GetMostActiveClients', 'P') IS NOT NULL
    DROP PROCEDURE GetMostActiveClients;
GO

CREATE PROCEDURE GetMostActiveClients
    @start_date DATETIME2, 
    @end_date DATETIME2,
    @top_n INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP(@top_n) 
        c.client_id, 
        c.name, 
        c.phone_number, 
        COUNT(cl.call_id) AS total_calls
    FROM Client c
    LEFT JOIN Calls cl ON c.client_id = cl.client_id
    WHERE cl.call_time BETWEEN @start_date AND @end_date
    GROUP BY c.client_id, c.name, c.phone_number
    ORDER BY total_calls DESC;
END;
GO
