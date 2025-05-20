USE MTS;
GO

IF OBJECT_ID('GetClientCallCostAnalysis', 'P') IS NOT NULL
    DROP PROCEDURE GetClientCallCostAnalysis;
GO

CREATE PROCEDURE GetClientCallCostAnalysis
    @start_date DATE,               -- Начальная дата для анализа
    @end_date DATE                  -- Конечная дата для анализа
AS
BEGIN
    SET NOCOUNT ON;

    -- Основной запрос для получения информации о стоимости звонков по клиентам
    SELECT 
        c.client_id,
        c.name AS client_name,
        c.phone_number,
        SUM(CASE 
                WHEN ca.call_type = 'По городу' THEN ca.call_cost
                ELSE 0
            END) AS local_calls_cost, -- Стоимость городских звонков
        SUM(CASE 
                WHEN ca.call_type = 'Междугородний' THEN ca.call_cost
                ELSE 0
            END) AS long_distance_calls_cost, -- Стоимость междугородних звонков
        SUM(CASE 
                WHEN ca.call_type = 'Международный' THEN ca.call_cost
                ELSE 0
            END) AS international_calls_cost, -- Стоимость международных звонков
        SUM(ca.call_cost) AS total_calls_cost  -- Общая стоимость всех звонков
    FROM Client c
    JOIN Calls ca ON c.client_id = ca.client_id
    WHERE ca.call_time BETWEEN @start_date AND @end_date
    GROUP BY c.client_id, c.name, c.phone_number
    ORDER BY total_calls_cost DESC;
    
END;
GO
