USE MTS;
GO

-- Вставка данных в таблицу Calls (Звонки)
DECLARE @j INT = 1;
DECLARE @ClientId INT;
DECLARE @TariffId INT;
DECLARE @CallType NVARCHAR(20);
DECLARE @RatePerMinute DECIMAL(10, 2);
DECLARE @CallDuration INT;
DECLARE @CallCost DECIMAL(10, 2);
DECLARE @PhoneNumber NVARCHAR(15);

WHILE @j <= 2337
BEGIN

    SELECT TOP 1 
        @ClientId = client_id,
        @PhoneNumber = phone_number,
        @TariffId = tariff_id
    FROM Client
    ORDER BY NEWID();

    -- Генерация случайного типа звонка
    SET @CallType = 
        CASE 
            WHEN RAND() < 0.33 THEN 'По городу' 
            WHEN RAND() < 0.66 THEN 'Междугородний' 
            ELSE 'Международный' 
        END;

    -- Получаем тарифные данные для клиента из таблицы Tariff
    SELECT @RatePerMinute = 
        CASE 
            WHEN @CallType = 'По городу' THEN cost_local
            WHEN @CallType = 'Междугородний' THEN cost_long_distance
            ELSE cost_international
        END
    FROM Tariff
    WHERE tariff_id = @TariffId;

    -- Генерация длительности звонка (от 1 до 30 минут)
    SET @CallDuration = FLOOR(RAND() * 30) + 1;

    -- Рассчитываем стоимость звонка (стоимость за минуту * длительность звонка)
    SET @CallCost = @RatePerMinute * @CallDuration;

    -- Вставка данных о звонке
    INSERT INTO Calls (client_id, receiver_number, call_time, call_duration, call_type, call_cost)
    VALUES 
    (
        @ClientId,  
        @PhoneNumber,
        DATEADD(MINUTE, FLOOR(RAND() * DATEDIFF(MINUTE, '2024-09-01 00:00:00', '2025-04-09 23:59:59')), '2024-09-01 00:00:00'),
        @CallDuration,  
        @CallType, 
        @CallCost
    );

    SET @j = @j + 1;
END
GO

-- Вставка данных в таблицу Call_Detail_Request (Запросы детализации звонков)
DECLARE @k INT = 1;  -- Используем новую переменную @k
DECLARE @ClientId INT;

WHILE @k <= 638
BEGIN
    -- Генерация случайного client_id из существующих клиентов в таблице Client
    SELECT TOP 1 @ClientId = client_id FROM Client ORDER BY NEWID();  -- Случайный client_id

    DECLARE @EndDate DATETIME;
    DECLARE @StartDate DATETIME;

    -- Генерация случайной конечной даты (в пределах до 09.04.2025)
    SET @EndDate = DATEADD(DAY, FLOOR(RAND() * DATEDIFF(DAY, '2025-03-01', '2025-04-09')), '2025-03-01');  -- Конечная дата в диапазоне до 09.04.2025

    -- Генерация случайной начальной даты, которая не будет позднее конечной
    SET @StartDate = DATEADD(DAY, FLOOR(RAND() * DATEDIFF(DAY, '2024-09-01', @EndDate)), '2024-09-01');  -- Начальная дата в диапазоне с 01.09.2024

    -- Вставка данных
    INSERT INTO Call_Detail_Request (client_id, start_date, end_date)
    VALUES 
    (@ClientId,  -- Используем существующий client_id
    @StartDate,  -- Начальная дата
    @EndDate);  -- Конечная дата

    SET @k = @k + 1;
END
GO
