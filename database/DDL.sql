CREATE TABLE Users (
    user_id SERIAL PRIMARY KEY,
    preferred_measurement TEXT CHECK (preferred_measurement IN ('pmol\\l', 'pg/ml')) NOT NULL
);

CREATE TABLE Logins (
    login_id VARCHAR(32) PRIMARY KEY,
    user_id INT UNIQUE REFERENCES Users(user_id) ON DELETE CASCADE,
    username TEXT UNIQUE NOT NULL,
    salt TEXT NOT NULL,
    password_hash TEXT NOT NULL,
    secret TEXT NOT NULL
);

CREATE TABLE Estrogen_Levels (
    level_id VARCHAR(60) PRIMARY KEY,
    user_id INT NOT NULL REFERENCES Users(user_id) ON DELETE CASCADE,
    date_tested DATE NOT NULL,
    level_pmol INT NOT NULL,
    level_pgml INT NOT NULL
);

CREATE TABLE Dosages (
    dosage_id VARCHAR(60)  PRIMARY KEY,
    user_id INT NOT NULL REFERENCES Users(user_id) ON DELETE CASCADE,
    concentration NUMERIC(10,2) NOT NULL,
    amount NUMERIC(10,2) NOT NULL,
    ester TEXT NOT NULL
);

CREATE TABLE Doses (
    dose_id VARCHAR(60) PRIMARY KEY,
    user_id INT NOT NULL REFERENCES Users(user_id) ON DELETE CASCADE,
    dosage_id INT NOT NULL REFERENCES Dosages(dosage_id) ON DELETE CASCADE,
    is_done BOOLEAN DEFAULT FALSE,
    date_scheduled DATE NOT NULL
);