CREATE TRIGGER trg_PreventCallWithNegativeBalance
ON Calls
AFTER INSERT
AS
BEGIN
    -- ���������, ���� �� ����� ����������� ������� �����, ��� � ������� ������������� ������
    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN Client c ON i.client_id = c.client_id
        WHERE c.balance < 0
    )
    BEGIN
        RAISERROR('������ ��������� ������ ��� ������������� �������.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO
