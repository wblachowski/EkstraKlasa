


CREATE TABLE coach (
    pesel         INTEGER NOT NULL,
    hiring_date   DATE NOT NULL,
    team_id       INTEGER NOT NULL
);

CREATE UNIQUE INDEX coach__idx ON
    coach ( team_id ASC );

ALTER TABLE coach ADD CONSTRAINT coach_pk PRIMARY KEY ( pesel );

CREATE TABLE goal (
    minute         NUMBER(2) NOT NULL,
    player_pesel   INTEGER NOT NULL,
    team_id        INTEGER NOT NULL,
    match_id       INTEGER NOT NULL
);

CREATE UNIQUE INDEX goal__idx ON
    goal ( player_pesel ASC );

CREATE UNIQUE INDEX goal__idxv1 ON
    goal ( team_id ASC );

CREATE TABLE match (
    id              INTEGER NOT NULL,
    "date"          DATE NOT NULL,
    score_host      INTEGER NOT NULL,
    score_guest     INTEGER NOT NULL,
    stadium_id      INTEGER NOT NULL,
    team_host_id    INTEGER NOT NULL,
    team_guest_id   INTEGER NOT NULL
);

ALTER TABLE match ADD CONSTRAINT match_pk PRIMARY KEY ( id );

CREATE TABLE person (
    pesel           INTEGER NOT NULL,
    firstname       VARCHAR2(100) NOT NULL,
    lastname        VARCHAR2(100) NOT NULL,
    date_of_birth   DATE NOT NULL,
    nationality     VARCHAR2(100) NOT NULL
);

ALTER TABLE person ADD CONSTRAINT person_pk PRIMARY KEY ( pesel );

CREATE TABLE player (
    pesel     INTEGER NOT NULL,
    weight    NUMBER(3) NOT NULL,
    height    NUMBER(3) NOT NULL,
    nr        NUMBER(2) NOT NULL,
    team_id   INTEGER NOT NULL
);

ALTER TABLE player ADD CONSTRAINT player_pk PRIMARY KEY ( pesel );

CREATE TABLE stadium (
    id         INTEGER NOT NULL,
    name       VARCHAR2(100) NOT NULL,
    city       VARCHAR2(100) NOT NULL,
    address    VARCHAR2(100) NOT NULL,
    capacity   NUMBER(6) NOT NULL
);

ALTER TABLE stadium ADD CONSTRAINT stadium_pk PRIMARY KEY ( id );

CREATE TABLE team (
    id             INTEGER NOT NULL,
    name           VARCHAR2(100) NOT NULL,
    founded_date   DATE NOT NULL,
    logo_path      VARCHAR2(100) NOT NULL,
    stadium_id     INTEGER NOT NULL
);

ALTER TABLE team ADD CONSTRAINT team_pk PRIMARY KEY ( id );

CREATE TABLE "User" (
    id         INTEGER NOT NULL,
    name       VARCHAR2(100) NOT NULL,
    password   VARCHAR2(100) NOT NULL
);

ALTER TABLE "User" ADD CONSTRAINT user_pk PRIMARY KEY ( id );

ALTER TABLE coach
    ADD CONSTRAINT coach_person_fk FOREIGN KEY ( pesel )
        REFERENCES person ( pesel )
		ON DELETE CASCADE;

ALTER TABLE coach
    ADD CONSTRAINT coach_team_fk FOREIGN KEY ( team_id )
        REFERENCES team ( id )
		ON DELETE CASCADE;

ALTER TABLE goal
    ADD CONSTRAINT goal_match_fk FOREIGN KEY ( match_id )
        REFERENCES match ( id )
		ON DELETE CASCADE;

ALTER TABLE goal
    ADD CONSTRAINT goal_player_fk FOREIGN KEY ( player_pesel )
        REFERENCES player ( pesel )
		ON DELETE CASCADE;

ALTER TABLE goal
    ADD CONSTRAINT goal_team_fk FOREIGN KEY ( team_id )
        REFERENCES team ( id )
		ON DELETE CASCADE;

ALTER TABLE match
    ADD CONSTRAINT match_stadium_fk FOREIGN KEY ( stadium_id )
        REFERENCES stadium ( id );

ALTER TABLE player
    ADD CONSTRAINT player_person_fk FOREIGN KEY ( pesel )
        REFERENCES person ( pesel )
		ON DELETE CASCADE;

ALTER TABLE player
    ADD CONSTRAINT player_team_fk FOREIGN KEY ( team_id )
        REFERENCES team ( id )
		ON DELETE CASCADE;

ALTER TABLE match
    ADD CONSTRAINT team_guest_fk FOREIGN KEY ( team_guest_id )
        REFERENCES team ( id )
		ON DELETE CASCADE;

ALTER TABLE match
    ADD CONSTRAINT team_host_fk FOREIGN KEY ( team_host_id )
        REFERENCES team ( id )
		ON DELETE CASCADE;

ALTER TABLE team
    ADD CONSTRAINT team_stadium_fk FOREIGN KEY ( stadium_id )
        REFERENCES stadium ( id );

--  ERROR: No Discriminator Column found in Arc FKArc_1 - constraint trigger for Arc cannot be generated 

--  ERROR: No Discriminator Column found in Arc FKArc_1 - constraint trigger for Arc cannot be generated



-- Oracle SQL Developer Data Modeler Summary Report: 
-- 
-- CREATE TABLE                             8
-- CREATE INDEX                             3
-- ALTER TABLE                             18
-- CREATE VIEW                              0
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                           0
-- ALTER TRIGGER                            0
-- CREATE COLLECTION TYPE                   0
-- CREATE STRUCTURED TYPE                   0
-- CREATE STRUCTURED TYPE BODY              0
-- CREATE CLUSTER                           0
-- CREATE CONTEXT                           0
-- CREATE DATABASE                          0
-- CREATE DIMENSION                         0
-- CREATE DIRECTORY                         0
-- CREATE DISK GROUP                        0
-- CREATE ROLE                              0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE SEQUENCE                          0
-- CREATE MATERIALIZED VIEW                 0
-- CREATE SYNONYM                           0
-- CREATE TABLESPACE                        0
-- CREATE USER                              0
-- 
-- DROP TABLESPACE                          0
-- DROP DATABASE                            0
-- 
-- REDACTION POLICY                         0
-- 
-- ORDS DROP SCHEMA                         0
-- ORDS ENABLE SCHEMA                       0
-- ORDS ENABLE OBJECT                       0
-- 
-- ERRORS                                   2
-- WARNINGS                                 0
