CREATE TRIGGER trg_PreventTariffChangeWithNegativeBalance
ON Client
AFTER UPDATE
AS
BEGIN
    -- ��������: ���� �� ������� ����� ������ ��� ������������� �������
    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN deleted d ON i.client_id = d.client_id
        WHERE i.tariff_id <> d.tariff_id AND i.balance < 0
    )
    BEGIN
        RAISERROR('������ ������� ����� ��� ������������� �������.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO