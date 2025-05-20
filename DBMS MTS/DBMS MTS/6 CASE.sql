USE MTS;
GO
SELECT call_id, call_cost,
    CASE 
        WHEN call_cost < 20 THEN 'Дешевый'
        WHEN call_cost BETWEEN 20 AND 45 THEN 'Средний'
        ELSE 'Дорогой'
    END AS call_category
FROM Calls;
