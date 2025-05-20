USE MTS;
GO
SELECT c.name, c.phone_number, t.tariff_name
FROM Client c
JOIN Tariff t ON c.tariff_id = t.tariff_id;
