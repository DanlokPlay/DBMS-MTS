USE MTS;
GO
UPDATE Client
SET balance = balance + 200
WHERE phone_number = N'+79001234567';
