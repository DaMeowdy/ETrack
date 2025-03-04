CREATE TABLE Users (
    user_id SERIAL PRIMARY KEY,
    preferred_measurement TEXT CHECK (preferred_measurement IN ('pmol\\l', 'pg/ml')) NOT NULL
);

CREATE TABLE Logins (
    login_id text PRIMARY KEY,
    user_id INT UNIQUE REFERENCES Users(user_id) ON DELETE CASCADE,
    username TEXT UNIQUE NOT NULL,
    salt TEXT NOT NULL,
    password_hash TEXT NOT NULL,
    secret TEXT NOT NULL
);

CREATE TABLE Estrogen_Levels (
    level_id text PRIMARY KEY,
    user_id INT NOT NULL REFERENCES Users(user_id) ON DELETE CASCADE,
    date_tested DATE NOT NULL,
    level_pmol INT NOT NULL,
    level_pgml INT NOT NULL
);

CREATE TABLE Dosages (
    dosage_id text  PRIMARY KEY,
    user_id INT NOT NULL REFERENCES Users(user_id) ON DELETE CASCADE,
    concentration text NOT NULL,
    amount NUMERIC(10,2) NOT NULL,
    ester TEXT NOT NULL
);

CREATE TABLE Doses (
    dose_id text PRIMARY KEY,
    user_id INT NOT NULL REFERENCES Users(user_id) ON DELETE CASCADE,
    dosage_id TEXT NOT NULL REFERENCES Dosages(dosage_id) ON DELETE CASCADE,
    is_done BOOLEAN DEFAULT FALSE,
    date_scheduled DATE NOT NULL
);