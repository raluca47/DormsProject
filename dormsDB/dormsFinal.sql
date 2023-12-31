PGDMP     3    :                {           dorms    14.5    14.5 B    W           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            X           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            Y           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            Z           1262    16887    dorms    DATABASE     i   CREATE DATABASE dorms WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'English_United States.1252';
    DROP DATABASE dorms;
                postgres    false            �            1255    17065 C   add_student(integer, integer, character varying, character varying) 	   PROCEDURE     \  CREATE PROCEDURE public.add_student(IN p_student_id integer, IN p_study_year integer, IN p_form_of_education character varying, IN p_cnp character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
  INSERT INTO student (student_id, study_year, form_of_education, cnp)
  VALUES (p_student_id, p_study_year, p_form_of_education, p_cnp);
commit;
END;
$$;
 �   DROP PROCEDURE public.add_student(IN p_student_id integer, IN p_study_year integer, IN p_form_of_education character varying, IN p_cnp character varying);
       public          postgres    false            �            1255    17058 "   assign_dorm_room(integer, integer) 	   PROCEDURE     -  CREATE PROCEDURE public.assign_dorm_room(IN p_student_id integer, IN p_room_number integer)
    LANGUAGE plpgsql
    AS $$DECLARE 
  room_exists INT DEFAULT NULL;
   is_room INT DEFAULT NULL;
BEGIN

  SELECT COUNT(*) into is_room  FROM room WHERE room_number = p_room_number;
  IF (is_room) > 0 THEN
  SET room_exists = 1;
END IF;

  IF room_exists IS NULL THEN
    INSERT INTO room (room_number,floor,dorm_id) VALUES (p_room_number,1,1);
  END IF;

  UPDATE contract
  SET room_number = p_room_number
  WHERE student_id = p_student_id;

  COMMIT;
END;
$$;
 [   DROP PROCEDURE public.assign_dorm_room(IN p_student_id integer, IN p_room_number integer);
       public          postgres    false            �            1255    17059    students_building("char") 	   PROCEDURE     �   CREATE PROCEDURE public.students_building(IN pbuilding "char")
    LANGUAGE sql
    AS $_$CREATE OR REPLACE PROCEDURE studenti_corp
(pbuilding char)
language plpgsql
as $$
begin
SELECT * FROM student
Where building=pbuilding;
commit;
end;
$$;
$_$;
 >   DROP PROCEDURE public.students_building(IN pbuilding "char");
       public          postgres    false            �            1259    16888    address    TABLE     �   CREATE TABLE public.address (
    address_id integer NOT NULL,
    street character varying(50) NOT NULL,
    number integer
);
    DROP TABLE public.address;
       public         heap    postgres    false            �            1259    16891    administrator    TABLE     �   CREATE TABLE public.administrator (
    administrator_id integer NOT NULL,
    cnp character varying(13) NOT NULL,
    dorm_id integer NOT NULL
);
 !   DROP TABLE public.administrator;
       public         heap    postgres    false            �            1259    16894    complex    TABLE     f   CREATE TABLE public.complex (
    complex_id integer NOT NULL,
    name character varying NOT NULL
);
    DROP TABLE public.complex;
       public         heap    postgres    false            �            1259    16899    contract    TABLE     �   CREATE TABLE public.contract (
    contract_id integer NOT NULL,
    student_id integer NOT NULL,
    room_number integer NOT NULL
);
    DROP TABLE public.contract;
       public         heap    postgres    false            �            1259    16902    dorm    TABLE     |   CREATE TABLE public.dorm (
    dorm_id integer NOT NULL,
    adress_id integer NOT NULL,
    complex_id integer NOT NULL
);
    DROP TABLE public.dorm;
       public         heap    postgres    false            �            1259    16905    faculty    TABLE     �   CREATE TABLE public.faculty (
    building "char" NOT NULL,
    domain_name character varying(70) NOT NULL,
    email character varying(50),
    phone_number character varying(10),
    address_id integer
);
    DROP TABLE public.faculty;
       public         heap    postgres    false            �            1259    16908    person    TABLE     �   CREATE TABLE public.person (
    cnp character varying(13) NOT NULL,
    first_name character varying(50) NOT NULL,
    last_name character varying(50) NOT NULL,
    "e-mail" character varying(50),
    adress_id integer NOT NULL
);
    DROP TABLE public.person;
       public         heap    postgres    false            �            1259    16911 	   professor    TABLE     �   CREATE TABLE public.professor (
    professor_id integer NOT NULL,
    cnp character varying(13) NOT NULL,
    building "char" NOT NULL,
    room_number integer NOT NULL
);
    DROP TABLE public.professor;
       public         heap    postgres    false            �            1259    16914    professor_faculty    TABLE     k   CREATE TABLE public.professor_faculty (
    professor_id integer NOT NULL,
    building "char" NOT NULL
);
 %   DROP TABLE public.professor_faculty;
       public         heap    postgres    false            �            1259    16917    room    TABLE     �   CREATE TABLE public.room (
    room_number integer NOT NULL,
    floor integer NOT NULL,
    dorm_id integer NOT NULL,
    isoccupied boolean DEFAULT false NOT NULL
);
    DROP TABLE public.room;
       public         heap    postgres    false            �            1259    16920    student    TABLE     �   CREATE TABLE public.student (
    student_id integer NOT NULL,
    study_year integer,
    form_of_education character varying(20),
    cnp character varying(13),
    room_number integer NOT NULL
);
    DROP TABLE public.student;
       public         heap    postgres    false            �            1259    16923    student_faculty    TABLE     g   CREATE TABLE public.student_faculty (
    student_id integer NOT NULL,
    building "char" NOT NULL
);
 #   DROP TABLE public.student_faculty;
       public         heap    postgres    false            �            1259    16926    student_professor    TABLE     n   CREATE TABLE public.student_professor (
    student_id integer NOT NULL,
    professor_id integer NOT NULL
);
 %   DROP TABLE public.student_professor;
       public         heap    postgres    false            H          0    16888    address 
   TABLE DATA           =   COPY public.address (address_id, street, number) FROM stdin;
    public          postgres    false    209   JU       I          0    16891    administrator 
   TABLE DATA           G   COPY public.administrator (administrator_id, cnp, dorm_id) FROM stdin;
    public          postgres    false    210   yU       J          0    16894    complex 
   TABLE DATA           3   COPY public.complex (complex_id, name) FROM stdin;
    public          postgres    false    211   �U       K          0    16899    contract 
   TABLE DATA           H   COPY public.contract (contract_id, student_id, room_number) FROM stdin;
    public          postgres    false    212   �U       L          0    16902    dorm 
   TABLE DATA           >   COPY public.dorm (dorm_id, adress_id, complex_id) FROM stdin;
    public          postgres    false    213   �U       M          0    16905    faculty 
   TABLE DATA           Y   COPY public.faculty (building, domain_name, email, phone_number, address_id) FROM stdin;
    public          postgres    false    214   V       N          0    16908    person 
   TABLE DATA           Q   COPY public.person (cnp, first_name, last_name, "e-mail", adress_id) FROM stdin;
    public          postgres    false    215   V       O          0    16911 	   professor 
   TABLE DATA           M   COPY public.professor (professor_id, cnp, building, room_number) FROM stdin;
    public          postgres    false    216   �V       P          0    16914    professor_faculty 
   TABLE DATA           C   COPY public.professor_faculty (professor_id, building) FROM stdin;
    public          postgres    false    217   �V       Q          0    16917    room 
   TABLE DATA           G   COPY public.room (room_number, floor, dorm_id, isoccupied) FROM stdin;
    public          postgres    false    218   �V       R          0    16920    student 
   TABLE DATA           ^   COPY public.student (student_id, study_year, form_of_education, cnp, room_number) FROM stdin;
    public          postgres    false    219   W       S          0    16923    student_faculty 
   TABLE DATA           ?   COPY public.student_faculty (student_id, building) FROM stdin;
    public          postgres    false    220   {W       T          0    16926    student_professor 
   TABLE DATA           E   COPY public.student_professor (student_id, professor_id) FROM stdin;
    public          postgres    false    221   �W       �           2606    16930    address address_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.address
    ADD CONSTRAINT address_pkey PRIMARY KEY (address_id);
 >   ALTER TABLE ONLY public.address DROP CONSTRAINT address_pkey;
       public            postgres    false    209            �           2606    16932     administrator administrator_pkey 
   CONSTRAINT     l   ALTER TABLE ONLY public.administrator
    ADD CONSTRAINT administrator_pkey PRIMARY KEY (administrator_id);
 J   ALTER TABLE ONLY public.administrator DROP CONSTRAINT administrator_pkey;
       public            postgres    false    210            �           2606    16934    complex complex_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.complex
    ADD CONSTRAINT complex_pkey PRIMARY KEY (complex_id);
 >   ALTER TABLE ONLY public.complex DROP CONSTRAINT complex_pkey;
       public            postgres    false    211            �           2606    16936    contract contract_pkey 
   CONSTRAINT     ]   ALTER TABLE ONLY public.contract
    ADD CONSTRAINT contract_pkey PRIMARY KEY (contract_id);
 @   ALTER TABLE ONLY public.contract DROP CONSTRAINT contract_pkey;
       public            postgres    false    212            �           2606    16938    dorm dorm_pkey 
   CONSTRAINT     Q   ALTER TABLE ONLY public.dorm
    ADD CONSTRAINT dorm_pkey PRIMARY KEY (dorm_id);
 8   ALTER TABLE ONLY public.dorm DROP CONSTRAINT dorm_pkey;
       public            postgres    false    213            �           2606    16940    faculty faculty_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.faculty
    ADD CONSTRAINT faculty_pkey PRIMARY KEY (building);
 >   ALTER TABLE ONLY public.faculty DROP CONSTRAINT faculty_pkey;
       public            postgres    false    214            �           2606    16942    person person_pkey 
   CONSTRAINT     Q   ALTER TABLE ONLY public.person
    ADD CONSTRAINT person_pkey PRIMARY KEY (cnp);
 <   ALTER TABLE ONLY public.person DROP CONSTRAINT person_pkey;
       public            postgres    false    215            �           2606    16944 (   professor_faculty professor_faculty_pkey 
   CONSTRAINT     z   ALTER TABLE ONLY public.professor_faculty
    ADD CONSTRAINT professor_faculty_pkey PRIMARY KEY (professor_id, building);
 R   ALTER TABLE ONLY public.professor_faculty DROP CONSTRAINT professor_faculty_pkey;
       public            postgres    false    217    217            �           2606    16946    professor professor_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public.professor
    ADD CONSTRAINT professor_pkey PRIMARY KEY (professor_id);
 B   ALTER TABLE ONLY public.professor DROP CONSTRAINT professor_pkey;
       public            postgres    false    216            �           2606    16948    room room_pkey 
   CONSTRAINT     U   ALTER TABLE ONLY public.room
    ADD CONSTRAINT room_pkey PRIMARY KEY (room_number);
 8   ALTER TABLE ONLY public.room DROP CONSTRAINT room_pkey;
       public            postgres    false    218            �           2606    16950 $   student_faculty student_faculty_pkey 
   CONSTRAINT     t   ALTER TABLE ONLY public.student_faculty
    ADD CONSTRAINT student_faculty_pkey PRIMARY KEY (student_id, building);
 N   ALTER TABLE ONLY public.student_faculty DROP CONSTRAINT student_faculty_pkey;
       public            postgres    false    220    220            �           2606    16952    student student_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.student
    ADD CONSTRAINT student_pkey PRIMARY KEY (student_id);
 >   ALTER TABLE ONLY public.student DROP CONSTRAINT student_pkey;
       public            postgres    false    219            �           2606    16954 (   student_professor student_professor_pkey 
   CONSTRAINT     |   ALTER TABLE ONLY public.student_professor
    ADD CONSTRAINT student_professor_pkey PRIMARY KEY (student_id, professor_id);
 R   ALTER TABLE ONLY public.student_professor DROP CONSTRAINT student_professor_pkey;
       public            postgres    false    221    221            �           2606    16955 (   administrator administrator_dorm_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.administrator
    ADD CONSTRAINT administrator_dorm_id_fkey FOREIGN KEY (dorm_id) REFERENCES public.dorm(dorm_id);
 R   ALTER TABLE ONLY public.administrator DROP CONSTRAINT administrator_dorm_id_fkey;
       public          postgres    false    213    3224    210            �           2606    16960 +   administrator administrator_person_cnp_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.administrator
    ADD CONSTRAINT administrator_person_cnp_fkey FOREIGN KEY (cnp) REFERENCES public.person(cnp);
 U   ALTER TABLE ONLY public.administrator DROP CONSTRAINT administrator_person_cnp_fkey;
       public          postgres    false    210    3228    215            �           2606    16965    dorm complex_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.dorm
    ADD CONSTRAINT complex_id FOREIGN KEY (complex_id) REFERENCES public.complex(complex_id) NOT VALID;
 9   ALTER TABLE ONLY public.dorm DROP CONSTRAINT complex_id;
       public          postgres    false    213    3220    211            �           2606    16970 "   contract contract_room_number_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.contract
    ADD CONSTRAINT contract_room_number_fkey FOREIGN KEY (room_number) REFERENCES public.room(room_number);
 L   ALTER TABLE ONLY public.contract DROP CONSTRAINT contract_room_number_fkey;
       public          postgres    false    212    3234    218            �           2606    16975 !   contract contract_student_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.contract
    ADD CONSTRAINT contract_student_id_fkey FOREIGN KEY (student_id) REFERENCES public.student(student_id) NOT VALID;
 K   ALTER TABLE ONLY public.contract DROP CONSTRAINT contract_student_id_fkey;
       public          postgres    false    212    3236    219            �           2606    16980    dorm dorm_adress_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.dorm
    ADD CONSTRAINT dorm_adress_id_fkey FOREIGN KEY (adress_id) REFERENCES public.address(address_id) NOT VALID;
 B   ALTER TABLE ONLY public.dorm DROP CONSTRAINT dorm_adress_id_fkey;
       public          postgres    false    213    209    3216            �           2606    16985    room dorm_id    FK CONSTRAINT     o   ALTER TABLE ONLY public.room
    ADD CONSTRAINT dorm_id FOREIGN KEY (dorm_id) REFERENCES public.dorm(dorm_id);
 6   ALTER TABLE ONLY public.room DROP CONSTRAINT dorm_id;
       public          postgres    false    3224    213    218            �           2606    16990    faculty faculty_address_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.faculty
    ADD CONSTRAINT faculty_address_id_fkey FOREIGN KEY (address_id) REFERENCES public.address(address_id);
 I   ALTER TABLE ONLY public.faculty DROP CONSTRAINT faculty_address_id_fkey;
       public          postgres    false    214    3216    209            �           2606    16995 %   student_faculty faculty_building_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.student_faculty
    ADD CONSTRAINT faculty_building_fkey FOREIGN KEY (building) REFERENCES public.faculty(building);
 O   ALTER TABLE ONLY public.student_faculty DROP CONSTRAINT faculty_building_fkey;
       public          postgres    false    214    220    3226            �           2606    17000 '   professor_faculty faculty_building_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.professor_faculty
    ADD CONSTRAINT faculty_building_fkey FOREIGN KEY (building) REFERENCES public.faculty(building);
 Q   ALTER TABLE ONLY public.professor_faculty DROP CONSTRAINT faculty_building_fkey;
       public          postgres    false    214    3226    217            �           2606    17005    person person_adress_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.person
    ADD CONSTRAINT person_adress_id_fkey FOREIGN KEY (adress_id) REFERENCES public.address(address_id);
 F   ALTER TABLE ONLY public.person DROP CONSTRAINT person_adress_id_fkey;
       public          postgres    false    209    215    3216            �           2606    17010 )   professor professor_faculty_building_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.professor
    ADD CONSTRAINT professor_faculty_building_fkey FOREIGN KEY (building) REFERENCES public.faculty(building);
 S   ALTER TABLE ONLY public.professor DROP CONSTRAINT professor_faculty_building_fkey;
       public          postgres    false    214    3226    216            �           2606    17015 #   professor_faculty professor_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.professor_faculty
    ADD CONSTRAINT professor_id_fkey FOREIGN KEY (professor_id) REFERENCES public.professor(professor_id);
 M   ALTER TABLE ONLY public.professor_faculty DROP CONSTRAINT professor_id_fkey;
       public          postgres    false    216    217    3230            �           2606    17020 #   student_professor professor_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.student_professor
    ADD CONSTRAINT professor_id_fkey FOREIGN KEY (professor_id) REFERENCES public.professor(professor_id);
 M   ALTER TABLE ONLY public.student_professor DROP CONSTRAINT professor_id_fkey;
       public          postgres    false    216    221    3230            �           2606    17025 #   professor professor_person_cnp_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.professor
    ADD CONSTRAINT professor_person_cnp_fkey FOREIGN KEY (cnp) REFERENCES public.person(cnp);
 M   ALTER TABLE ONLY public.professor DROP CONSTRAINT professor_person_cnp_fkey;
       public          postgres    false    3228    216    215            �           2606    17030 $   professor professor_room_number_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.professor
    ADD CONSTRAINT professor_room_number_fkey FOREIGN KEY (room_number) REFERENCES public.room(room_number);
 N   ALTER TABLE ONLY public.professor DROP CONSTRAINT professor_room_number_fkey;
       public          postgres    false    3234    218    216            �           2606    17035    student_faculty student_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.student_faculty
    ADD CONSTRAINT student_id_fkey FOREIGN KEY (student_id) REFERENCES public.student(student_id) NOT VALID;
 I   ALTER TABLE ONLY public.student_faculty DROP CONSTRAINT student_id_fkey;
       public          postgres    false    3236    220    219            �           2606    17040 !   student_professor student_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.student_professor
    ADD CONSTRAINT student_id_fkey FOREIGN KEY (student_id) REFERENCES public.student(student_id) NOT VALID;
 K   ALTER TABLE ONLY public.student_professor DROP CONSTRAINT student_id_fkey;
       public          postgres    false    3236    221    219            �           2606    17045    student student_person_cnp    FK CONSTRAINT     w   ALTER TABLE ONLY public.student
    ADD CONSTRAINT student_person_cnp FOREIGN KEY (cnp) REFERENCES public.person(cnp);
 D   ALTER TABLE ONLY public.student DROP CONSTRAINT student_person_cnp;
       public          postgres    false    215    219    3228            �           2606    33445    student student_room_number    FK CONSTRAINT     �   ALTER TABLE ONLY public.student
    ADD CONSTRAINT student_room_number FOREIGN KEY (room_number) REFERENCES public.room(room_number) NOT VALID;
 E   ALTER TABLE ONLY public.student DROP CONSTRAINT student_room_number;
       public          postgres    false    218    219    3234            H      x�3�L�4�2�L�4�2�������� +%m      I      x������ � �      J      x�3�L����� �P      K      x�3�4�4����� �]      L      x�3�4�4�2�4�1z\\\ %      M      x������ � �      N   ]   x��K@0��?�������J��H�7v_��ႍXv�R�F�B��""u�ƋK)96��2N�𧢟��2,�ڇs��_�}#��      O      x������ � �      P      x������ � �      Q   ;   x�3�4�4�,�2� ��7��M9���1����e�ia�s��$Ӹ��=... ��      R   [   x�M��	�@��sR�dv�1��EAl@��]T���op��9��U�)$aL0,ǶD�XgFzY��W�P譯�j*G���M��ڦ�      S      x������ � �      T      x������ � �     