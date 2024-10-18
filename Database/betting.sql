-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Okt 18. 10:28
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `betting`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `bets`
--


CREATE DATABASE betting;

USE betting;

CREATE TABLE `bets` (
  `id` int(11) NOT NULL,
  `eventId` int(11) DEFAULT NULL,
  `userId` int(11) DEFAULT NULL,
  `participantId` int(11) DEFAULT NULL,
  `prediction` varchar(255) DEFAULT NULL,
  `betAmount` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `events`
--

CREATE TABLE `events` (
  `id` int(11) NOT NULL,
  `eventName` varchar(255) DEFAULT NULL,
  `gameId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `events`
--

INSERT INTO `events` (`id`, `eventName`, `gameId`) VALUES
(1, 'Gábor programja hiba nélkül fut le', 1),
(2, 'Szabolcs programja 25-öt ír ki', 1),
(3, 'Gábor programja 5 másodperc alatt befejeződik', 1),
(4, 'Szabolcs programja nem crashel', 1),
(5, 'Péter programja sikeresen csatlakozik az adatbázishoz', 2),
(6, 'Tamás programja a helyes választ adja meg', 2),
(7, 'Péter programja 3 percen belül befejeződik', 2),
(8, 'Tamás programja nem tartalmaz szintaktikai hibát', 2),
(9, 'Zoltán programja a helyes eredményt adja meg', 3),
(10, 'László programja 10 másodperc alatt befejeződik', 3),
(11, 'Zoltán programja sikeresen olvassa be a fájlt', 3),
(12, 'László programja nem használja a tiltott funkciót', 3),
(13, 'András programja a helyes szöveget írja ki', 4),
(14, 'István programja 50 pontot ér el', 4),
(15, 'András programja 2 percen belül befejeződik', 4),
(16, 'István programja nem tartalmaz logikai hibát', 4),
(17, 'Ferenc programja sikeresen csatlakozik a szerverhez', 5),
(18, 'György programja a helyes eredményt adja meg', 5),
(19, 'Ferenc programja 4 percen belül befejeződik', 5),
(20, 'György programja nem használja a tiltott funkciót', 5),
(21, 'Csaba programja a helyes számot írja ki', 6),
(22, 'Dénes programja 20 másodperc alatt befejeződik', 6),
(23, 'Csaba programja sikeresen olvassa be a fájlt', 6),
(24, 'Dénes programja nem tartalmaz szintaktikai hibát', 6),
(25, 'Erzsébet programja a helyes eredményt adja meg', 7),
(26, 'Fanni programja 30 pontot ér el', 7),
(27, 'Erzsébet programja 1 percen belül befejeződik', 7),
(28, 'Fanni programja nem használja a tiltott funkciót', 7),
(29, 'Gábor programja sikeresen csatlakozik az adatbázishoz', 8);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `gameparticipants`
--

CREATE TABLE `gameparticipants` (
  `gameId` int(11) NOT NULL,
  `participantId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `gameparticipants`
--

INSERT INTO `gameparticipants` (`gameId`, `participantId`) VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4),
(3, 5),
(3, 6),
(4, 7),
(4, 8),
(5, 9),
(5, 10),
(6, 11),
(6, 12),
(7, 13),
(7, 14),
(8, 15),
(8, 16),
(9, 17),
(9, 18);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `games`
--

CREATE TABLE `games` (
  `id` int(11) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `userId` int(11) DEFAULT NULL,
  `status` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `games`
--

INSERT INTO `games` (`id`, `name`, `userId`, `status`) VALUES
(1, 'Kódverseny', 2, 0),
(2, 'Fejlesztő Fesztivál', 3, 0),
(3, 'Algoritmus Verseny', 4, 0),
(4, 'Kódoló Kupa', 5, 0),
(5, 'Fejlesztő Fesztivál', 3, 0),
(6, 'Algoritmus Bajnokság', 4, 0),
(7, 'Kódoló Kupa', 5, 0),
(8, 'Fejlesztő Fesztivál', 3, 0),
(9, 'Algoritmus Verseny', 4, 0),
(10, 'Kódoló Kupa', 5, 0);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `participants`
--

CREATE TABLE `participants` (
  `id` int(11) NOT NULL,
  `name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `participants`
--

INSERT INTO `participants` (`id`, `name`) VALUES
(1, 'Gábor'),
(2, 'Szabolcs'),
(3, 'Péter'),
(4, 'Tamás'),
(5, 'Zoltán'),
(6, 'László'),
(7, 'András'),
(8, 'István'),
(9, 'Ferenc'),
(10, 'György'),
(11, 'Csaba'),
(12, 'Dénes'),
(13, 'Erzsébet'),
(14, 'Fanni'),
(15, 'Hajnalka'),
(16, 'József'),
(17, 'Katalin'),
(18, 'László');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `results`
--

CREATE TABLE `results` (
  `id` int(11) NOT NULL,
  `eventId` int(11) DEFAULT NULL,
  `result` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `points` int(11) DEFAULT NULL,
  `role` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `users`
--

INSERT INTO `users` (`id`, `name`, `password`, `points`, `role`) VALUES
(1, 'admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 0, 0),
(2, 'Kódokátor', '0b14d501a594442a01c6859541bcb3e8164d183d32937b851835442f69d5c94e', 0, 1),
(3, 'Kódfejlesztő', '6cf615d5bcaac778352a8f1f3360d23f02f34ec182e259897fd6ce485d7870d4', 0, 1),
(4, 'AlgoMester', '5906ac361a137e2d286465cd6588ebb5ac3f5ae955001100bc41577c3d751764', 0, 1),
(5, 'AlgoBajnok', 'b97873a40f73abedd8d685a7cd5e5f85e4a9cfb83eac26886640a0813850122b', 0, 1),
(6, 'Kódolók Klubja', '8b2c86ea9cf2ea4eb517fd1e06b74f399e7fec0fef92e3b482a6cf2e2b092023', 0, 1);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `bets`
--
ALTER TABLE `bets`
  ADD PRIMARY KEY (`id`),
  ADD KEY `eventId` (`eventId`),
  ADD KEY `userId` (`userId`),
  ADD KEY `participantId` (`participantId`);

--
-- A tábla indexei `events`
--
ALTER TABLE `events`
  ADD PRIMARY KEY (`id`),
  ADD KEY `gameId` (`gameId`);

--
-- A tábla indexei `gameparticipants`
--
ALTER TABLE `gameparticipants`
  ADD PRIMARY KEY (`gameId`,`participantId`),
  ADD KEY `participantId` (`participantId`);

--
-- A tábla indexei `games`
--
ALTER TABLE `games`
  ADD PRIMARY KEY (`id`),
  ADD KEY `userId` (`userId`);

--
-- A tábla indexei `participants`
--
ALTER TABLE `participants`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `results`
--
ALTER TABLE `results`
  ADD PRIMARY KEY (`id`),
  ADD KEY `eventId` (`eventId`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `bets`
--
ALTER TABLE `bets`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `events`
--
ALTER TABLE `events`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT a táblához `games`
--
ALTER TABLE `games`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT a táblához `participants`
--
ALTER TABLE `participants`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT a táblához `results`
--
ALTER TABLE `results`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `bets`
--
ALTER TABLE `bets`
  ADD CONSTRAINT `bets_ibfk_1` FOREIGN KEY (`eventId`) REFERENCES `events` (`id`),
  ADD CONSTRAINT `bets_ibfk_2` FOREIGN KEY (`userId`) REFERENCES `users` (`id`),
  ADD CONSTRAINT `bets_ibfk_3` FOREIGN KEY (`participantId`) REFERENCES `participants` (`id`);

--
-- Megkötések a táblához `events`
--
ALTER TABLE `events`
  ADD CONSTRAINT `events_ibfk_1` FOREIGN KEY (`gameId`) REFERENCES `games` (`id`);

--
-- Megkötések a táblához `gameparticipants`
--
ALTER TABLE `gameparticipants`
  ADD CONSTRAINT `gameparticipants_ibfk_1` FOREIGN KEY (`gameId`) REFERENCES `games` (`id`),
  ADD CONSTRAINT `gameparticipants_ibfk_2` FOREIGN KEY (`participantId`) REFERENCES `participants` (`id`);

--
-- Megkötések a táblához `games`
--
ALTER TABLE `games`
  ADD CONSTRAINT `games_ibfk_1` FOREIGN KEY (`userId`) REFERENCES `users` (`id`);

--
-- Megkötések a táblához `results`
--
ALTER TABLE `results`
  ADD CONSTRAINT `results_ibfk_1` FOREIGN KEY (`eventId`) REFERENCES `events` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
