-- phpMyAdmin SQL Dump
-- version 4.7.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Mar 08, 2018 at 06:54 PM
-- Server version: 5.6.34-log
-- PHP Version: 7.1.5

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `mjbs`
--
CREATE DATABASE IF NOT EXISTS `mjbs` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `mjbs`;

-- --------------------------------------------------------

--
-- Table structure for table `battle_text`
--

CREATE TABLE `battle_text` (
  `id` int(11) NOT NULL,
  `start_battle` varchar(255) NOT NULL,
  `mid_battle` varchar(255) NOT NULL,
  `end_battle` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `players`
--

CREATE TABLE `players` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `hp_total` int(11) NOT NULL,
  `hp_remaining` int(11) NOT NULL,
  `flavor_id` int(11) DEFAULT NULL,
  `agility` int(11) NOT NULL,
  `intelligence` int(11) NOT NULL,
  `strength` int(11) NOT NULL,
  `luck` int(11) NOT NULL,
  `allegience` tinyint(1) NOT NULL,
  `x_pos` int(11) DEFAULT NULL,
  `y_pos` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `battle_text`
--
ALTER TABLE `battle_text`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `players`
--
ALTER TABLE `players`
  ADD PRIMARY KEY (`id`),
  ADD KEY `players_fk0` (`flavor_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `battle_text`
--
ALTER TABLE `battle_text`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `players`
--
ALTER TABLE `players`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `players`
--
ALTER TABLE `players`
  ADD CONSTRAINT `players_fk0` FOREIGN KEY (`flavor_id`) REFERENCES `battle_text` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
