------------ ------------
--Console IDs
    ------------ ------------
        -- Nintendo	10x --
    ------------ ------------
    --NES	            101
    --SNES	            102
    --N64	            103
    --GameCube	        104
    --Gameboy	        105
    --Gameboy Colour	106
    --Gameboy Advance	107
    --Nintendo DS	    108
    --Nintendo DS Lite	109
    --Nintendo DS XL	110

    ------------ ------------
        -- Atari 20x --
    ------------ ------------
    --Atari 2600	    201

    ------------ ------------
        -- Commodore 30x -- 
    ------------ ------------
    --Commodore 64	    301

    ------------ ------------
        -- Sony 40x -- 
    ------------ ------------
    --Playstation 1	    401
    --Playstation 2 	402
    --Playstation 3     403

    ------------ ------------
        -- Microsoft 50x -- 
    ------------ ------------
    --Xbox              501
    --Xbox 360          502
    --Xbox One	        503

------------ ------------ ------------
		  --Adding Consoles--
------------ ------------ ------------

--resets Console database--
delete from Console
--insert consoles
insert into Console (ConID, Console, Company)
values 
(101, 'NES', 'Nintendo'),
(102, 'SNES', 'Nintendo'),
(103, 'N64', 'Nintendo'),
(104, 'GameCube', 'Nintendo'),
(105, 'Gameboy', 'Nintendo'),
(106, 'Gameboy Color', 'Nintendo'),
(107, 'Gameboy Advance', 'Nintendo'),
(108, 'Nintendo DS', 'Nintendo'),
(201, 'Atari 2600', 'Atari')

