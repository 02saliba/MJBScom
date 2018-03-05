CREATE TABLE `player` (
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	`name` varchar(255) NOT NULL,
	`stat_id` varchar(255) NOT NULL,
	`hp_total` INT(11) NOT NULL,
	`hp_remaining` INT(11) NOT NULL,
	`team_id` INT(11) NOT NULL,
	`flavor_id` INT(11) NOT NULL,
	`agility` INT(11) NOT NULL,
	`intelligence` INT(11) NOT NULL,
	`strength` INT(11) NOT NULL,
	`luck` INT(11) NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE `items` (
	`name` varchar(255) NOT NULL,
	`description` varchar(255) NOT NULL,
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	PRIMARY KEY (`id`)
);

CREATE TABLE `battle_text` (
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	`start_battle` varchar(255) NOT NULL,
	`end_battle` varchar(255) NOT NULL,
	`weakened_response` varchar(255) NOT NULL,
	`post_response` varchar(255) NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE `team` (
	`name` varchar(255) NOT NULL,
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	`coordinates` varchar(255) NOT NULL,
	`allegiance` BOOLEAN NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE `players_teams` (
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	`player_id` INT(11) NOT NULL,
	`team_id` INT(11) NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE `inventory` (
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	`team_id` INT(11) NOT NULL,
	`item_id` INT(11) NOT NULL,
	PRIMARY KEY (`id`)
);

ALTER TABLE `player` ADD CONSTRAINT `player_fk0` FOREIGN KEY (`stat_id`) REFERENCES `stats`(`id`);

ALTER TABLE `player` ADD CONSTRAINT `player_fk1` FOREIGN KEY (`flavor_id`) REFERENCES `battle_text`(`id`);

ALTER TABLE `players_teams` ADD CONSTRAINT `players_teams_fk0` FOREIGN KEY (`player_id`) REFERENCES `player`(`id`);

ALTER TABLE `players_teams` ADD CONSTRAINT `players_teams_fk1` FOREIGN KEY (`team_id`) REFERENCES `team`(`id`);

ALTER TABLE `inventory` ADD CONSTRAINT `inventory_fk0` FOREIGN KEY (`team_id`) REFERENCES `team`(`id`);

ALTER TABLE `inventory` ADD CONSTRAINT `inventory_fk1` FOREIGN KEY (`item_id`) REFERENCES `Items`(`id`);
