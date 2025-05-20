CREATE TRIGGER trg_PreventCallWithNegativeBalance
ON Calls
AFTER INSERT
AS
BEGIN
    -- Проверяем, есть ли среди вставленных звонков такие, где у клиента отрицательный баланс
    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN Client c ON i.client_id = c.client_id
        WHERE c.balance < 0
    )
    BEGIN
        RAISERROR('Нельзя совершить звонок при отрицательном балансе.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO
