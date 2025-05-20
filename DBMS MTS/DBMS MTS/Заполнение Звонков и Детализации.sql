USE MTS;
GO

-- ������� ������ � ������� Calls (������)
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

    -- ��������� ���������� ���� ������
    SET @CallType = 
        CASE 
            WHEN RAND() < 0.33 THEN '�� ������' 
            WHEN RAND() < 0.66 THEN '�������������' 
            ELSE '�������������' 
        END;

    -- �������� �������� ������ ��� ������� �� ������� Tariff
    SELECT @RatePerMinute = 
        CASE 
            WHEN @CallType = '�� ������' THEN cost_local
            WHEN @CallType = '�������������' THEN cost_long_distance
            ELSE cost_international
        END
    FROM Tariff
    WHERE tariff_id = @TariffId;

    -- ��������� ������������ ������ (�� 1 �� 30 �����)
    SET @CallDuration = FLOOR(RAND() * 30) + 1;

    -- ������������ ��������� ������ (��������� �� ������ * ������������ ������)
    SET @CallCost = @RatePerMinute * @CallDuration;

    -- ������� ������ � ������
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

-- ������� ������ � ������� Call_Detail_Request (������� ����������� �������)
DECLARE @k INT = 1;  -- ���������� ����� ���������� @k
DECLARE @ClientId INT;

WHILE @k <= 638
BEGIN
    -- ��������� ���������� client_id �� ������������ �������� � ������� Client
    SELECT TOP 1 @ClientId = client_id FROM Client ORDER BY NEWID();  -- ��������� client_id

    DECLARE @EndDate DATETIME;
    DECLARE @StartDate DATETIME;

    -- ��������� ��������� �������� ���� (� �������� �� 09.04.2025)
    SET @EndDate = DATEADD(DAY, FLOOR(RAND() * DATEDIFF(DAY, '2025-03-01', '2025-04-09')), '2025-03-01');  -- �������� ���� � ��������� �� 09.04.2025

    -- ��������� ��������� ��������� ����, ������� �� ����� ������� ��������
    SET @StartDate = DATEADD(DAY, FLOOR(RAND() * DATEDIFF(DAY, '2024-09-01', @EndDate)), '2024-09-01');  -- ��������� ���� � ��������� � 01.09.2024

    -- ������� ������
    INSERT INTO Call_Detail_Request (client_id, start_date, end_date)
    VALUES 
    (@ClientId,  -- ���������� ������������ client_id
    @StartDate,  -- ��������� ����
    @EndDate);  -- �������� ����

    SET @k = @k + 1;
END
GO
