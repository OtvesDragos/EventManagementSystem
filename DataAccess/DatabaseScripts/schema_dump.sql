--
-- PostgreSQL database dump
--

-- Dumped from database version 17.5
-- Dumped by pg_dump version 17.5

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO pg_database_owner;

--
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


--
-- Name: audit_all_tables(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.audit_all_tables() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    pk JSONB;
BEGIN
    -- Assume the PK column is always named 'id' (customize if needed)
    pk := jsonb_build_object('id', COALESCE(NEW.id, OLD.id));

    IF TG_OP = 'INSERT' THEN
        INSERT INTO audit_log (table_name, operation, primary_key, new_data)
        VALUES (TG_TABLE_NAME, 'INSERT', pk, row_to_json(NEW)::jsonb);

    ELSIF TG_OP = 'UPDATE' THEN
        INSERT INTO audit_log (table_name, operation, primary_key, old_data, new_data)
        VALUES (TG_TABLE_NAME, 'UPDATE', pk, row_to_json(OLD)::jsonb, row_to_json(NEW)::jsonb);

    ELSIF TG_OP = 'DELETE' THEN
        INSERT INTO audit_log (table_name, operation, primary_key, old_data)
        VALUES (TG_TABLE_NAME, 'DELETE', pk, row_to_json(OLD)::jsonb);
    END IF;

    RETURN NULL;
END;
$$;


ALTER FUNCTION public.audit_all_tables() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: audit_log; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.audit_log (
    audit_id integer NOT NULL,
    table_name text NOT NULL,
    operation text NOT NULL,
    changed_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    primary_key jsonb,
    old_data jsonb,
    new_data jsonb
);


ALTER TABLE public.audit_log OWNER TO postgres;

--
-- Name: audit_log_audit_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.audit_log_audit_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.audit_log_audit_id_seq OWNER TO postgres;

--
-- Name: audit_log_audit_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.audit_log_audit_id_seq OWNED BY public.audit_log.audit_id;


--
-- Name: event_responses; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.event_responses (
    id uuid NOT NULL,
    name character varying(100) NOT NULL,
    event_code integer NOT NULL,
    response character varying(15) NOT NULL,
    email character varying(255),
    CONSTRAINT event_responses_response_check CHECK ((lower((response)::text) = ANY (ARRAY['going'::text, 'interested'::text, 'declined'::text])))
);


ALTER TABLE public.event_responses OWNER TO postgres;

--
-- Name: events; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.events (
    event_id uuid NOT NULL,
    name character varying(255) NOT NULL,
    description character varying(1000),
    date timestamp without time zone NOT NULL,
    location character varying(255) NOT NULL,
    event_code integer NOT NULL,
    created_by uuid,
    visibility character varying(15) DEFAULT 'private'::character varying NOT NULL
);


ALTER TABLE public.events OWNER TO postgres;

--
-- Name: events_event_code_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.events_event_code_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.events_event_code_seq OWNER TO postgres;

--
-- Name: events_event_code_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.events_event_code_seq OWNED BY public.events.event_code;


--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    id uuid NOT NULL,
    email character varying(64) NOT NULL,
    password_hash character varying(512) NOT NULL,
    first_name character varying(100),
    last_name character varying(100)
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Name: audit_log audit_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.audit_log ALTER COLUMN audit_id SET DEFAULT nextval('public.audit_log_audit_id_seq'::regclass);


--
-- Name: events event_code; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.events ALTER COLUMN event_code SET DEFAULT nextval('public.events_event_code_seq'::regclass);


--
-- Name: audit_log audit_log_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.audit_log
    ADD CONSTRAINT audit_log_pkey PRIMARY KEY (audit_id);


--
-- Name: event_responses event_responses_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.event_responses
    ADD CONSTRAINT event_responses_pkey PRIMARY KEY (id);


--
-- Name: events events_event_code_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.events
    ADD CONSTRAINT events_event_code_key UNIQUE (event_code);


--
-- Name: events events_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.events
    ADD CONSTRAINT events_pkey PRIMARY KEY (event_id);


--
-- Name: event_responses uq_eventcode_email; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.event_responses
    ADD CONSTRAINT uq_eventcode_email UNIQUE (event_code, email);


--
-- Name: users users_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_email_key UNIQUE (email);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- Name: event_responses audit_event_responses; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER audit_event_responses AFTER INSERT OR DELETE OR UPDATE ON public.event_responses FOR EACH ROW EXECUTE FUNCTION public.audit_all_tables();


--
-- Name: events audit_events; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER audit_events AFTER INSERT OR DELETE OR UPDATE ON public.events FOR EACH ROW EXECUTE FUNCTION public.audit_all_tables();


--
-- Name: users audit_users; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER audit_users AFTER INSERT OR DELETE OR UPDATE ON public.users FOR EACH ROW EXECUTE FUNCTION public.audit_all_tables();


--
-- Name: event_responses fk_event; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.event_responses
    ADD CONSTRAINT fk_event FOREIGN KEY (event_code) REFERENCES public.events(event_code);


--
-- Name: events fk_events_created_by; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.events
    ADD CONSTRAINT fk_events_created_by FOREIGN KEY (created_by) REFERENCES public.users(id);


--
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: pg_database_owner
--

GRANT USAGE ON SCHEMA public TO event_user;


--
-- Name: TABLE audit_log; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.audit_log TO event_user;


--
-- Name: SEQUENCE audit_log_audit_id_seq; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON SEQUENCE public.audit_log_audit_id_seq TO event_user;


--
-- Name: TABLE event_responses; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.event_responses TO event_user;


--
-- Name: TABLE events; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.events TO event_user;


--
-- Name: SEQUENCE events_event_code_seq; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON SEQUENCE public.events_event_code_seq TO event_user;


--
-- Name: TABLE users; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.users TO event_user;


--
-- Name: DEFAULT PRIVILEGES FOR TABLES; Type: DEFAULT ACL; Schema: public; Owner: postgres
--

ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA public GRANT SELECT,INSERT,DELETE,UPDATE ON TABLES TO event_user;


--
-- PostgreSQL database dump complete
--

