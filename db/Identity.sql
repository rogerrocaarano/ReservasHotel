CREATE TABLE "AspNetRoles"
(
    "Id"               TEXT PRIMARY KEY NOT NULL,
    "ConcurrencyStamp" TEXT,
    "Name"             TEXT,
    "NormalizedName"   TEXT
);

CREATE TABLE "AspNetRoleClaims"
(
    "Id"         SERIAL PRIMARY KEY,
    "ClaimType"  TEXT,
    "ClaimValue" TEXT,
    "RoleId"     TEXT REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_AspNetRoleClaims_RoleId"
    ON "AspNetRoleClaims" ("RoleId");

CREATE INDEX "RoleNameIndex"
    ON "AspNetRoles" ("NormalizedName");

CREATE TABLE "AspNetUserTokens"
(
    "UserId"        TEXT NOT NULL,
    "LoginProvider" TEXT NOT NULL,
    "Name"          TEXT NOT NULL,
    "Value"         TEXT,
    PRIMARY KEY ("UserId", "LoginProvider", "Name")
);

CREATE TABLE "AspNetUsers"
(
    "Id"                   TEXT PRIMARY KEY NOT NULL,
    "AccessFailedCount"    INTEGER          NOT NULL,
    "ConcurrencyStamp"     TEXT,
    "Email"                TEXT,
    "EmailConfirmed"       INTEGER          NOT NULL,
    "LockoutEnabled"       INTEGER          NOT NULL,
    "LockoutEnd"           TEXT,
    "NormalizedEmail"      TEXT,
    "NormalizedUserName"   TEXT,
    "PasswordHash"         TEXT,
    "PhoneNumber"          TEXT,
    "PhoneNumberConfirmed" INTEGER          NOT NULL,
    "SecurityStamp"        TEXT,
    "TwoFactorEnabled"     INTEGER          NOT NULL,
    "UserName"             TEXT
);

CREATE TABLE "AspNetUserClaims"
(
    "Id"         SERIAL PRIMARY KEY,
    "ClaimType"  TEXT,
    "ClaimValue" TEXT,
    "UserId"     TEXT REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_AspNetUserClaims_UserId"
    ON "AspNetUserClaims" ("UserId");

CREATE TABLE "AspNetUserLogins"
(
    "LoginProvider"       TEXT NOT NULL,
    "ProviderKey"         TEXT NOT NULL,
    "ProviderDisplayName" TEXT,
    "UserId"              TEXT REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE,
    PRIMARY KEY ("LoginProvider", "ProviderKey")
);

CREATE INDEX "IX_AspNetUserLogins_UserId"
    ON "AspNetUserLogins" ("UserId");

CREATE TABLE "AspNetUserRoles"
(
    "UserId" TEXT REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE,
    "RoleId" TEXT REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
    PRIMARY KEY ("UserId", "RoleId")
);

CREATE INDEX "IX_AspNetUserRoles_RoleId"
    ON "AspNetUserRoles" ("RoleId");

CREATE INDEX "IX_AspNetUserRoles_UserId"
    ON "AspNetUserRoles" ("UserId");

CREATE INDEX "EmailIndex"
    ON "AspNetUsers" ("NormalizedEmail");

CREATE UNIQUE INDEX "UserNameIndex"
    ON "AspNetUsers" ("NormalizedUserName");
