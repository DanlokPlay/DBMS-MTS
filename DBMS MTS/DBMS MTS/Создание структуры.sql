USE MTS;
GO

-- Таблица для тарифов
CREATE TABLE Tariff (
    tariff_id INT IDENTITY(1,1) PRIMARY KEY,       -- Идентификатор тарифа
    tariff_name NVARCHAR(50) NOT NULL,              -- Название тарифа
    tariff_type NVARCHAR(20) CHECK(tariff_type IN ('Корпоративный', 'Некорпоративный')), -- Тип тарифа (Проверка ограничения на вводимые данные)
    switch_cost MONEY NOT NULL,                     -- Стоимость перехода на тариф
    cost_local MONEY NOT NULL,                      -- Стоимость минуты по городу
    cost_long_distance MONEY NOT NULL,             -- Стоимость минуты междугородний
    cost_international MONEY NOT NULL              -- Стоимость минуты международный
);
GO

-- Таблица для клиентов
CREATE TABLE Client (
    client_id INT IDENTITY(1,1) PRIMARY KEY,       -- Идентификатор клиента
    name NVARCHAR(100) NOT NULL,                    -- Имя клиента
    client_type NVARCHAR(20) CHECK(client_type IN ('Физическое', 'Юридическое')), -- Тип клиента (Проверка ограничения на вводимые данные)
    balance MONEY NOT NULL DEFAULT 0,                    -- Баланс клиента (по умолчанию 0)
    phone_number NVARCHAR(15) UNIQUE NOT NULL,     -- Номер телефона клиента
    tariff_id INT,                                  -- Идентификатор тарифа клиента
    FOREIGN KEY (tariff_id) REFERENCES Tariff(tariff_id) -- Связь с тарифом
    ON DELETE SET NULL -- Если тариф удалён, ставим NULL в поле тариф клиента
);
GO

-- Таблица для звонков
CREATE TABLE Calls (
    call_id INT IDENTITY(1,1) PRIMARY KEY,         -- Идентификатор звонка
    client_id INT,                                  -- Идентификатор клиента
    receiver_number NVARCHAR(15) NOT NULL,          -- Номер абонента, которому звонили
    call_time DATETIME2 NOT NULL,                   -- Время звонка (используем DATETIME2)
    call_duration INT NOT NULL,                     -- Длительность звонка (в минутах)
    call_type NVARCHAR(20) CHECK(call_type IN ('По городу', 'Междугородний', 'Международный')), -- Тип соединения (Проверка ограничения на вводимые данные)
    call_cost MONEY NOT NULL DEFAULT 0,             -- Стоимость звонка (по умолчанию 0)
    FOREIGN KEY (client_id) REFERENCES Client(client_id) -- Связь с клиентом
    ON DELETE CASCADE -- Если клиент удалён, все его звонки тоже удаляются
);
GO

-- Таблица для запросов детализации звонков
CREATE TABLE Call_Detail_Request (
    request_id INT IDENTITY(1,1) PRIMARY KEY,      -- Идентификатор запроса
    client_id INT,                                  -- Идентификатор клиента
    start_date DATE NOT NULL,                       -- Начальная дата для детализации
    end_date DATE NOT NULL,                         -- Конечная дата для детализации
    FOREIGN KEY (client_id) REFERENCES Client(client_id) -- Связь с клиентом
    ON DELETE CASCADE -- Если клиент удалён, запросы его детализации тоже удаляются
);
GO