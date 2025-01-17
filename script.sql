CREATE EXTENSION IF NOT EXISTS postgis;
CREATE TABLE IF NOT EXISTS public."__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;
CREATE TABLE "TaxOffices" (
    id uuid NOT NULL,
    name character varying(500) NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    deleted_at timestamp with time zone,
    CONSTRAINT pk_tax_offices PRIMARY KEY (id)
);

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20241212204157_Initial', '9.0.0');

ALTER TABLE "TaxOffices" RENAME TO tax_offices;

CREATE TABLE business_categories (
    id uuid NOT NULL,
    name character varying(500) NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    deleted_at timestamp with time zone,
    CONSTRAINT pk_business_categories PRIMARY KEY (id)
);

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20241223124122_BusinessCategory', '9.0.0');

CREATE TABLE countries (
    id uuid NOT NULL,
    name character varying(500) NOT NULL,
    code character varying(10) NOT NULL,
    has_region boolean NOT NULL DEFAULT TRUE,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    deleted_at timestamp with time zone,
    CONSTRAINT pk_countries PRIMARY KEY (id)
);

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20241224205152_Country', '9.0.0');

CREATE TABLE campaign_types (
    id uuid NOT NULL,
    name character varying(500) NOT NULL,
    description character varying(500) NOT NULL,
    code character varying(80) NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    deleted_at timestamp with time zone,
    CONSTRAINT pk_campaign_types PRIMARY KEY (id)
);

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20241225233743_CampaignType', '9.0.0');

CREATE TABLE regions (
    id uuid NOT NULL,
    name character varying(500) NOT NULL,
    country_id uuid NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    deleted_at timestamp with time zone,
    CONSTRAINT pk_regions PRIMARY KEY (id),
    CONSTRAINT fk_regions_countries_country_id FOREIGN KEY (country_id) REFERENCES countries (id) ON DELETE CASCADE
);

CREATE UNIQUE INDEX ix_countries_name ON countries (name);

CREATE UNIQUE INDEX ix_regions_country_id ON regions (country_id);

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20241226223522_RegionInit', '9.0.0');

DROP INDEX ix_regions_country_id;

CREATE INDEX ix_regions_country_id ON regions (country_id);

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20241228213417_RegionRelationshipWithMany', '9.0.0');

CREATE TABLE cities (
    id uuid NOT NULL,
    name character varying(500) NOT NULL,
    code integer NOT NULL,
    country_id uuid NOT NULL,
    region_id uuid,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    deleted_at timestamp with time zone,
    CONSTRAINT pk_cities PRIMARY KEY (id),
    CONSTRAINT fk_cities_country_country_id FOREIGN KEY (country_id) REFERENCES countries (id) ON DELETE CASCADE,
    CONSTRAINT fk_cities_region_region_id FOREIGN KEY (region_id) REFERENCES regions (id) ON DELETE SET NULL
);

CREATE INDEX ix_cities_country_id ON cities (country_id);

CREATE INDEX ix_cities_region_id ON cities (region_id);

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20241229120104_City', '9.0.0');

ALTER TABLE cities ALTER COLUMN code TYPE character varying(10);

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20241229123426_CityCodeTyeChanged', '9.0.0');

CREATE TABLE districts (
    id uuid NOT NULL,
    name character varying(500) NOT NULL,
    code character varying(50) NOT NULL,
    city_id uuid NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    deleted_at timestamp with time zone,
    CONSTRAINT pk_districts PRIMARY KEY (id),
    CONSTRAINT fk_districts_cities_city_id FOREIGN KEY (city_id) REFERENCES cities (id) ON DELETE CASCADE
);

CREATE INDEX ix_districts_city_id ON districts (city_id);

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20241229183826_District', '9.0.0');

CREATE TABLE outbox_messages (
    id uuid NOT NULL,
    occurred_on_utc timestamp with time zone NOT NULL,
    type text NOT NULL,
    content jsonb NOT NULL,
    processed_on_utc timestamp with time zone,
    error text,
    CONSTRAINT pk_outbox_messages PRIMARY KEY (id)
);

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20241229192601_OutboxMessages', '9.0.0');

CREATE TABLE users (
    id uuid NOT NULL,
    name character varying(500),
    email character varying(500),
    phone_number character varying(500) NOT NULL,
    is_phone_number_verified boolean DEFAULT FALSE,
    birth_date timestamp with time zone,
    gender integer,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    deleted_at timestamp with time zone,
    CONSTRAINT pk_users PRIMARY KEY (id)
);

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20250101234414_UserInit', '9.0.0');

CREATE UNIQUE INDEX ix_users_phone_number ON users (phone_number);

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20250105143005_UserIndexAdded', '9.0.0');

ALTER TABLE users RENAME COLUMN name TO last_name;

ALTER TABLE users ADD first_name character varying(500);

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20250106193219_UserFirstLastName', '9.0.0');

ALTER TABLE users ADD profile_photo_id uuid;

CREATE TABLE files (
    id uuid NOT NULL,
    name character varying(500) NOT NULL,
    path character varying(500) NOT NULL,
    type character varying(500) NOT NULL,
    size double precision NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    deleted_at timestamp with time zone,
    CONSTRAINT pk_files PRIMARY KEY (id)
);

CREATE INDEX ix_users_profile_photo_id ON users (profile_photo_id);

ALTER TABLE users ADD CONSTRAINT fk_users_files_profile_photo_id FOREIGN KEY (profile_photo_id) REFERENCES files (id) ON DELETE SET NULL;

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20250108191337_FileAndProfilePhoto', '9.0.0');

CREATE TABLE businesses (
    id uuid NOT NULL,
    logo_id uuid NOT NULL,
    owner_id uuid NOT NULL,
    country_id uuid NOT NULL,
    city_id uuid NOT NULL,
    district_id uuid NOT NULL,
    tax_office_id uuid NOT NULL,
    base_info_name character varying(100) NOT NULL,
    base_info_email character varying(200) NOT NULL,
    base_info_phone_number character varying(100) NOT NULL,
    base_info_description character varying(2000),
    base_info_online_order_link text,
    base_info_menu_link text,
    base_info_website_link text,
    address_info_address text NOT NULL,
    address_info_latitude numeric NOT NULL,
    address_info_longitude numeric NOT NULL,
    tax_info_trade_name character varying(500) NOT NULL,
    tax_info_vkn integer NOT NULL,
    is_kvkk_approved boolean NOT NULL DEFAULT FALSE,
    working_hours_start_time interval,
    working_hours_end_time interval,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    deleted_at timestamp with time zone,
    CONSTRAINT pk_businesses PRIMARY KEY (id),
    CONSTRAINT fk_businesses_city_city_id FOREIGN KEY (city_id) REFERENCES cities (id) ON DELETE CASCADE,
    CONSTRAINT fk_businesses_country_country_id FOREIGN KEY (country_id) REFERENCES countries (id) ON DELETE CASCADE,
    CONSTRAINT fk_businesses_district_district_id FOREIGN KEY (district_id) REFERENCES districts (id) ON DELETE CASCADE,
    CONSTRAINT fk_businesses_file_logo_id FOREIGN KEY (logo_id) REFERENCES files (id) ON DELETE CASCADE,
    CONSTRAINT fk_businesses_tax_office_tax_office_id FOREIGN KEY (tax_office_id) REFERENCES tax_offices (id) ON DELETE CASCADE,
    CONSTRAINT fk_businesses_users_owner_id FOREIGN KEY (owner_id) REFERENCES users (id) ON DELETE CASCADE
);

CREATE TABLE business_business_category (
    business_category_id uuid NOT NULL,
    business_id uuid NOT NULL,
    CONSTRAINT pk_business_business_category PRIMARY KEY (business_category_id, business_id),
    CONSTRAINT fk_business_business_category_business_categories_business_cat FOREIGN KEY (business_category_id) REFERENCES business_categories (id) ON DELETE CASCADE,
    CONSTRAINT fk_business_business_category_businesses_business_id FOREIGN KEY (business_id) REFERENCES businesses (id) ON DELETE CASCADE
);

CREATE TABLE business_file (
    business_id uuid NOT NULL,
    file_id uuid NOT NULL,
    CONSTRAINT pk_business_file PRIMARY KEY (business_id, file_id),
    CONSTRAINT fk_business_file_businesses_business_id FOREIGN KEY (business_id) REFERENCES businesses (id) ON DELETE CASCADE,
    CONSTRAINT fk_business_file_file_file_id FOREIGN KEY (file_id) REFERENCES files (id) ON DELETE CASCADE
);

CREATE INDEX ix_business_business_category_business_id ON business_business_category (business_id);

CREATE INDEX ix_business_file_file_id ON business_file (file_id);

CREATE INDEX ix_businesses_city_id ON businesses (city_id);

CREATE INDEX ix_businesses_country_id ON businesses (country_id);

CREATE INDEX ix_businesses_district_id ON businesses (district_id);

CREATE INDEX ix_businesses_logo_id ON businesses (logo_id);

CREATE UNIQUE INDEX ix_businesses_owner_id ON businesses (owner_id);

CREATE INDEX ix_businesses_tax_office_id ON businesses (tax_office_id);

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20250109004622_BusinessEntity', '9.0.0');

ALTER TABLE businesses ALTER COLUMN tax_info_vkn TYPE text;

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20250109182352_ChangeVknFieldType', '9.0.0');

ALTER TABLE users ADD switched_to_business boolean NOT NULL DEFAULT FALSE;

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20250114001128_UserSwitchBusinessAdded', '9.0.0');

ALTER TABLE businesses DROP CONSTRAINT fk_businesses_tax_office_tax_office_id;

DROP INDEX ix_businesses_tax_office_id;

ALTER TABLE businesses DROP COLUMN address_info_latitude;

ALTER TABLE businesses DROP COLUMN address_info_longitude;

ALTER TABLE businesses DROP COLUMN tax_info_trade_name;

ALTER TABLE businesses DROP COLUMN tax_info_vkn;

ALTER TABLE businesses DROP COLUMN tax_office_id;

ALTER TABLE businesses RENAME COLUMN address_info_address TO address;

CREATE EXTENSION IF NOT EXISTS postgis;

ALTER TABLE businesses ADD location geometry(Point, 4326) NOT NULL;

CREATE INDEX ix_businesses_location ON businesses USING gist (location);

INSERT INTO public."__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20250117002827_PostGISAndBusinessChanges', '9.0.0');

COMMIT;

