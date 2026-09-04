SELECT 'CREATE DATABASE "SeriesTest"'
WHERE NOT EXISTS
(
    SELECT 1
    FROM pg_database
    WHERE datname = 'SeriesTest'
)\gexec
