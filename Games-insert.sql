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
		   --Adding Games--
------------ ------------ ------------

--resets Game database--
delete from Game
-- insert games
insert into Game (Title, ConID, Release)
values 
('Duck Hunt', 101, 1984),
('Super Mario Bros.', 101, 1985),
('Ice Climber', 101, 1985),
('Snake Rattle N Roll', 101, 1990),
('Super Mario All Stars', 102, 1993),
('Donkey Kong Country', 102, 1994),
('Mechwarrior', 102, 1994),
('Lagoon', 102, 1990),
('Super Mario 64', 103, 1996),
('Mario Kart 64', 103, 1996),
('Mission Impossible', 103, 1998),
('International Superstar Soccer 98', 103, 1998),
('Wave Race 64', 103, 1996),
('Topgear Rally 2', 103, 1999),
('FIFA 64', 103, 1997),
('Pokémon Stadium', 103, 1999),
('Mario Kart: Double Dash', 104, 2003),
('Super Mario Sunshine', 104, 2002),
('Metroid Prime', 104, 2002),
('Super Smash Bros: Melee', 104, 2001),
('Spyro: Enter the Dragonfly', 104, 2002),
('Legend of Zelda: The Wind Waker', 104, 2002),
('Pokemon Red', 105, 1996),
('Super Mario Land 2', 105, 1992),
('Kirby’s Dream Land', 105, 1992),
('Mario & Yoshi', 105, 1991),
('Pokemon Crystal', 106, 2001),
('WarioLand 2', 106, 1998),
('Metroid 2: Return of Samus', 106, 1992),
('Mario Kart Super Circuit', 107, 2001),
('Pokemon Sapphire', 107, 2002),
('Pokemon Ruby', 107, 2002),
('Super Mario World: Super Mario Adventure 2', 107, 2001),
('Pokemon Mystery Dungeon', 107, 2005),
('Littlest Petshop: Garden', 108, 2008),
('Spyro: Eternal Night', 108, 2007),
('Spyro: A New Beginning', 108, 2006),
('Hello Kitty Birthday Adventures', 108, 2010),
('Littlest Petshop: Jungle', 108, 2008),
('Littlest Petshop: City', 108, 2009),
('Littlest Petshop: Beach', 108, 2009),
('Littlest Petshop: Biggest Stars Purple Team', 108, 2010),
('Solar Fox', 201, 1981),
('Space Invaders', 201, 1978),
('MotoRodeo', 201, 1990),
('Indy 500', 201, 1977),
('Defender II', 201, 1987),
('Mario Bros.', 201, 1983),
('Jr. Pac-Man', 201, 1983),
('Lilly Adventure', 201, 1983),
('Video Olympics', 201, 1977),
('Decathlon', 201, 1983),
('RealSport Tennis', 201, 1983),
('Sprintmaster', 201, 1988),
('Pong Sports', 201, 1972),
('RealSports Soccer', 201, 1982),
('King Arthur', 201, 1983),
('Pole Position', 201, 1982),
('Football - RealSports Soccer', 201, 1982),
('Circus Atari', 201, 1980),
('QBert', 201, 1983)