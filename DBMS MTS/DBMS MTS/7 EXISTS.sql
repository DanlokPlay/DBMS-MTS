USE MTS;
GO
SELECT t.tariff_id, t.tariff_name
FROM Tariff t
WHERE EXISTS (
    SELECT 1 FROM Client c WHERE c.tariff_id = t.tariff_id
);
