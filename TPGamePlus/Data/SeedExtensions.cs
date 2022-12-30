using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TPGamePlus.Domain;
using TPGamePlus.Domain.Entities;
using static TPGamePlus.Models.Account.RegistrationViewModel;
using Console = TPGamePlus.Domain.Entities.Console;

namespace TPGamePlus.Data
{
    public static class SeedExtensions
    {
        private static void SeedUsers(this ModelBuilder builder, IEnumerable<ApplicationUser> users)
        {
            foreach (var user in users)
            {
                builder.Entity<ApplicationUser>().HasData(user);
            }
        }

        private static void SeedUsersToRole(this ModelBuilder builder, IEnumerable<ApplicationUser> users, IdentityRole role)
        {
            builder.Entity<IdentityRole>().HasData(role);

            foreach (var user in users)
            {
                builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
                {
                    UserId = user.Id,
                    RoleId = role.Id
                });
            }
        }

        public static void Seed(this ModelBuilder builder)
        {
            var admins = new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    FirstName = "admin",
                    LastName = "admin",
                    PhoneNumber = "123-123-1234",
                    LockoutEnabled = true,
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Qwerty123!"),
                }
            };
            builder.SeedUsers(admins);
            builder.SeedUsersToRole(admins, new IdentityRole("Administrateur"));

            builder.Entity<Status>().HasData(
                new Status
                {
                    StatusID = 1,
                    StatusName = "Disponible",
                    isMainShopFilter = true
                },
                new Status
                {
                    StatusID = 2,
                    StatusName = "Pre-commander",
                    isMainShopFilter = true
                },
                new Status
                {
                    StatusID = 3,
                    StatusName = "Indisponible",
                    isMainShopFilter = true
                });

            builder.Entity<Publisher>().HasData(
                new Publisher
                {
                    PublisherID = 1,
                    PublisherName = "Nintendo",
                    isMainShopFilter = true
                },
                new Publisher
                {
                    PublisherID = 2,
                    PublisherName = "Microsoft",
                    isMainShopFilter = true
                },
                new Publisher
                {
                    PublisherID = 3,
                    PublisherName = "Ubisoft",
                    isMainShopFilter = true
                },
                new Publisher
                {
                    PublisherID = 4,
                    PublisherName = "EA",
                    isMainShopFilter = true
                },
                new Publisher
                {
                    PublisherID = 5,
                    PublisherName = "Undead Labs",
                    isMainShopFilter = true
                },
                new Publisher
                {
                    PublisherID = 6,
                    PublisherName = "Milestone",
                    isMainShopFilter = true
                },
                new Publisher
                {
                    PublisherID = 7,
                    PublisherName = "Guerrilla Games",
                    isMainShopFilter = true
                },
                new Publisher
                {
                    PublisherID = 8,
                    PublisherName = "Sony",
                    isMainShopFilter = true
                },
                new Publisher
                {
                    PublisherID = 9,
                    PublisherName = "Square Enix",
                    isMainShopFilter = true
                },
                new Publisher
                {
                    PublisherID = 10,
                    PublisherName = "Team17",
                    isMainShopFilter = true
                },
                new Publisher
                {
                    PublisherID = 11,
                    PublisherName = "Capcom",
                    isMainShopFilter = true
                });

            builder.Entity<Compagny>().HasData(
                new Compagny
                {
                    CompagnyID = 1,
                    CompagnyName = "Nintendo",
                    isMainShopFilter = true
                },
                new Compagny
                {
                    CompagnyID = 2,
                    CompagnyName = "Microsoft",
                    isMainShopFilter = true
                },
                new Compagny
                {
                    CompagnyID = 3,
                    CompagnyName = "Ubisoft",
                    isMainShopFilter = true
                },
                new Compagny
                {
                    CompagnyID = 4,
                    CompagnyName = "EA",
                    isMainShopFilter = true
                },
                new Compagny
                {
                    CompagnyID = 5,
                    CompagnyName = "Undead Labs",
                    isMainShopFilter = true
                },
                new Compagny
                {
                    CompagnyID = 6,
                    CompagnyName = "Milestone",
                    isMainShopFilter = true
                },
                new Compagny
                {
                    CompagnyID = 7,
                    CompagnyName = "Guerrilla Games",
                    isMainShopFilter = true
                },
                new Compagny
                {
                    CompagnyID = 8,
                    CompagnyName = "Sony",
                    isMainShopFilter = true
                },
                new Compagny
                {
                    CompagnyID = 9,
                    CompagnyName = "Square Enix",
                    isMainShopFilter = true
                },
                new Compagny
                {
                    CompagnyID = 10,
                    CompagnyName = "Team17",
                    isMainShopFilter = true
                },
                new Compagny
                {
                    CompagnyID = 11,
                    CompagnyName = "Capcom",
                    isMainShopFilter = true
                });

            builder.Entity<Plateforme>().HasData(
                new Plateforme
                {
                    PlateformeID = 1,
                    PlateformeName = "Switch",
                    isMainShopFilter = true
                },
                new Plateforme
                {
                    PlateformeID = 2,
                    PlateformeName = "Xbox",
                    isMainShopFilter = true
                },
                new Plateforme
                {
                    PlateformeID = 3,
                    PlateformeName = "Ps4",
                    isMainShopFilter = true
                },
                new Plateforme
                {
                    PlateformeID = 4,
                    PlateformeName = "Ps5",
                    isMainShopFilter = true
                },
                new Plateforme
                {
                    PlateformeID = 5,
                    PlateformeName = "Pc",
                    isMainShopFilter = true
                });

            builder.Entity<Category>().HasData(
                new Category
                {
                    CategoryID = 1,
                    CategoryName = "Console",
                    isMainShopFilter = true
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName = "Jeu",
                    isMainShopFilter = true
                },
                new Category
                {
                    CategoryID = 3,
                    CategoryName = "Controlleur",
                    isMainShopFilter = true
                },
                new Category
                {
                    CategoryID = 4,
                    CategoryName = "Accessoires",
                    isMainShopFilter = true
                });

            builder.Entity<Console>().HasData(
                new Console
                {
                    ConsoleID = 1,
                    ConsoleName = "Console",
                    isMainShopFilter = true
                });

            builder.Entity<GameCategory>().HasData(
                new GameCategory
                {
                    CategoryGameID = 1,
                    CategoryGameName = "Console",
                    isMainShopFilter = true
                },
                new GameCategory
                {
                    CategoryGameID = 2,
                    CategoryGameName = "Jeu",
                    isMainShopFilter = true
                },
                new GameCategory
                {
                    CategoryGameID = 3,
                    CategoryGameName = "Controlleur",
                    isMainShopFilter = true
                },
                new GameCategory
                {
                    CategoryGameID = 4,
                    CategoryGameName = "Accessoires",
                    isMainShopFilter = true
                });

            builder.Entity<ProductInfo>().HasData(
                new ProductInfo
                {
                    ProductInfoID = 1,
                    Description = "Super Mario Odyssey est un jeu de plateforme dans lequel les joueurs contrôlent Mario alors qu'il voyage à travers de nombreux mondes différents, connus sous le nom de \"Kingdoms\" dans le jeu, sur le navire en forme de chapeau Odyssey, pour sauver la princesse Peach de Bowser, qui envisage de l'épouser de force .",
                },
                new ProductInfo
                {
                    ProductInfoID = 2,
                    Description = "Explorez une île Koholint réinventée dans l'un des jeux les plus appréciés de la série Legend of Zelda . Link s'est échoué sur une île mystérieuse aux habitants étranges et colorés. Pour rentrer chez lui, Link doit collecter des instruments magiques à travers le pays et réveiller le Wind Fish.",
                },
                new ProductInfo
                {
                    ProductInfoID = 3,
                    Description = "Le jeu Super Mario 3D World + Bowser's Fury propose le même gameplay coopératif animé, des niveaux créatifs et des bonus que le jeu original, mais avec des améliorations supplémentaires . Dans la partie Super Mario 3D World du jeu, les personnages se déplacent plus rapidement et le tableau de bord s'allume plus rapidement.",
                },
                new ProductInfo
                {
                    ProductInfoID = 4,
                    Description = "Participez à deux aventures familiales à défilement latéral avec jusqu'à trois amis* en essayant de sauver le Royaume Champignon . Comprend leNouveau Super Mario Bros. U et les nouveaux jeux Super Luigi U plus durs et plus rapides, qui incluent tous deux Nabbit et Toadette en tant que personnages jouables ! Deux jeux en un, pour doubler le plaisir !",
                },
                new ProductInfo
                {
                    ProductInfoID = 5,
                    Description = "Les modes vont du golf standard au golf rapide énergique et une aventure de golf de type RPG en mode histoire . Des commandes intuitives de mouvement ou de bouton, une jauge de tir qui s'adapte à la courbe du parcours et d'autres nouvelles fonctionnalités permettent aux nouveaux joueurs et aux pros chevronnés de conduire et de putter avec puissance.",
                },
                new ProductInfo
                {
                    ProductInfoID = 6,
                    Description = "Le jeu propose cinq planches refaites de la trilogie originale de la Nintendo 64 et un total de 100 mini-jeux sélectionnés à partir des entrées précédentes de la série , similaires au jeu Nintendo 3DS Mario Party: The Top 100 (2017). Contrairement à Super Mario Party, Superstars peut être joué avec des boutons de commande.",
                },
                new ProductInfo
                {
                    ProductInfoID = 7,
                    Description = "Minecraft est un jeu vidéo sandbox développé par Mojang Studios . Le jeu a été créé par Markus \"Notch\" Persson dans le langage de programmation Java . Après plusieurs premières versions de test privées, il a été rendu public pour la première fois en mai 2009 avant d'être entièrement publié en novembre 2011, avec la démission de Notch et Jens \"Jeb\" Bergensten prenant en charge le développement. Minecraft a depuis été porté sur plusieurs autres plateformes et est le jeu vidéo le plus vendu de tous les temps , avec plus de 238 millions d'exemplaires vendus et près de 140 millions de joueurs actifs par mois en 2021.",
                },
                new ProductInfo
                {
                    ProductInfoID = 8,
                    Description = "State of Decay 2 est un jeu de survie zombie, mettant l'accent sur la recherche d'objets, dans lequel le gameplay est vécu à la troisième personne . Le jeu se déroule dans un environnement de monde ouvert et propose un gameplay coopératif avec jusqu'à trois autres joueurs.",
                },
                new ProductInfo
                {
                    ProductInfoID = 9,
                    Description = "Tout en se concentrant sur un tableau qui montre une terre médiévale, ils sont soudainement aspirés par le tableau, entrant dans le monde, et l'aventure commence . Le gang doit courir, sauter et se frayer un chemin à travers chaque monde pour sauver la situation et découvrir les secrets de chaque peinture légendaire.",
                },
                new ProductInfo
                {
                    ProductInfoID = 10,
                    Description = "Assassin's Creed IV: Black Flag est un jeu d'action-aventure et d'infiltration se déroulant dans un environnement de monde ouvert et joué du point de vue de la troisième personne . Le jeu propose trois villes principales; La Havane, Kingston et Nassau, qui résident respectivement sous l'influence espagnole, britannique et pirate.",
                },
                new ProductInfo
                {
                    ProductInfoID = 11,
                    Description = "Steep est un jeu de sports extrêmes multijoueur en ligne se déroulant dans un environnement mondial ouvert des Alpes, centré sur le Mont Blanc, la plus haute montagne d'Europe, qui peut être explorée librement par les joueurs .",
                },
                new ProductInfo
                {
                    ProductInfoID = 12,
                    Description = "MXGP2 est l'évolution du précédent titre Milestone Motocross MXGP - Le jeu vidéo officiel de motocross . Le jeu est basé sur la licence du Championnat du Monde FIM de Motocross 2015. MXGP2 est facile à apprécier, mais en même temps, il se caractérise par un gameplay totalement réaliste.",
                },
                new ProductInfo
                {
                    ProductInfoID = 13,
                    Description = "Horizon Forbidden West est un jeu de rôle d'action joué du point de vue de la troisième personne . Le joueur contrôle Aloy, un chasseur dans un monde peuplé de machines dangereuses et animales.",
                },
                new ProductInfo
                {
                    ProductInfoID = 14,
                    Description = "Star Wars : Squadrons est un jeu de combat spatial, joué du point de vue de la première personne . Les joueurs prennent le contrôle de chasseurs stellaires de l'Empire Galactique ou de la marine de la Nouvelle République.",
                },
                new ProductInfo
                {
                    ProductInfoID = 15,
                    Description = "Tom Clancy's Ghost Recon Wildlands est un jeu vidéo de tir tactique à la troisième personne développé par Ubisoft Paris en collaboration avec Ubisoft Bucarest , Ubisoft Reflections , Ubisoft Montpellier , Ubisoft Annecy , Ubisoft Milan et Ubisoft Belgrade , et édité par Ubisoft . Il est sorti dans le monde entier le 7 mars 2017 pour Microsoft Windows , PlayStation 4 et Xbox One , en tant que dixième opus de la franchise Tom Clancy's Ghost Recon et est le premier jeu de Ghost Recon .série pour présenter un environnement de monde ouvert .",
                },
                new ProductInfo
                {
                    ProductInfoID = 16,
                    Description = "Hot Wheels Unleashed est un jeu de course joué à la troisième personne . Dans le jeu, le joueur prend le contrôle des véhicules de la franchise Hot Wheels et fait la course contre d'autres adversaires sur des pistes miniatures situées dans divers lieux et environnements quotidiens, tels qu'un garage, une cuisine et une chambre.",
                },
                new ProductInfo
                {
                    ProductInfoID = 17,
                    Description = "Tom Clancy's Rainbow Six Siege propose une expérience en constante évolution avec des opportunités illimitées pour perfectionner votre stratégie et aider votre équipe à remporter la victoire. Le jeu est optimisé pour Next Gen (jusqu'à 4K et jusqu'à 120FPS). Plongez dans un gameplay 5v5 explosif, une compétition à enjeux élevés et des batailles d'équipe PVP passionnantes.",
                },
                new ProductInfo
                {
                    ProductInfoID = 18,
                    Description = "Immortals Fenyx Rising™ donne vie à une grande aventure mythologique . Incarnez Fenyx, un nouveau demi-dieu ailé, dans une quête pour sauver les dieux grecs. Le destin du monde est en jeu - vous êtes le dernier espoir des dieux.",
                },
                new ProductInfo
                {
                    ProductInfoID = 19,
                    Description = "Voici les Joy-Con, des manettes qui rendent possibles de nouveaux types de jeux, à utiliser avec la Nintendo Switch. Les Joy-Con polyvalents offrent de multiples nouvelles façons surprenantes pour les joueurs de s'amuser. Deux Joy-Con peuvent être utilisés indépendamment dans chaque main, ou ensemble comme une seule manette de jeu lorsqu'ils sont fixés à la poignée Joy-Con. Ils peuvent également se fixer à la console principale pour être utilisés en mode portable, ou être partagés avec des amis pour profiter d'une action à deux joueurs dans les jeux pris en charge. Chaque Joy-Con dispose d'un ensemble complet de boutons et peut être utilisé comme une manette autonome, et chacun comprend un accéléromètre et un capteur gyroscopique, ce qui permet un contrôle indépendant des mouvements à gauche et à droite.\"",
                },
                new ProductInfo
                {
                    ProductInfoID = 20,
                    Description = "Posez vos tentacules sur une manette Nintendo Switch™ Pro jaune et bleue.\r\n\r\nInclut les commandes par mouvements, les vibrations HD, fonctionnalité amiibo™* intégrée, et plus.",
                },
                new ProductInfo
                {
                    ProductInfoID = 21,
                    Description = "La manette DualSense pour PS5 est aussi incroyablement polyvalente. Un microphone et un capteur de mouvement sont intégrés et elle possède son propre haut-parleur avec un son haute fidélité. ",
                },
                new ProductInfo
                {
                    ProductInfoID = 22,
                    Description = "Conçue pour répondre aux besoins des joueurs compétitifs d'aujourd'hui, la manette sans fil Xbox Elite série 2 propose plus de 30 nouvelles façons de jouer comme un pro . Améliorez votre visée avec de nouvelles manettes à tension réglable, tirez encore plus vite avec des verrous de gâchette plus courts et restez sur la cible avec une poignée caoutchoutée enveloppante.",
                },
                new ProductInfo
                {
                    ProductInfoID = 23,
                    Description = "La Nintendo Switch (ニンテンドースイッチ, Nintendō Suitchi) est une console de jeux vidéo produite par Nintendo, succédant à la Wii U. Elle est la première console hybride de l'histoire des jeux vidéo, pouvant faire office aussi bien de console de salon que de console portable. Elle est sortie mondialement le 3 mars 2017 .",
                },
                new ProductInfo
                {
                    ProductInfoID = 24,
                    Description = "La nouvelle console offre un écran OLED de 7 pouces aux couleurs éclatantes, un grand support ajustable, une station d’accueil dotée d’un port Ethernet intégré, 64 Go de stockage interne et un son amélioré.",
                },
                new ProductInfo
                {
                    ProductInfoID = 25,
                    Description = "La PS5 est une console de salon avec support optique (Blu-ray 4K) annoncé pour la fin d'année 2020. La PS5 succède logiquement à la PS4 et introduit avec elle une nouvelle architecture matériel avec un CPU Octa-Core AMD cadencé à 3,5 GHz épaulé par 16 Go de RAM GDDR6 et d'un GPU AMD RDNA 2.",
                },
                new ProductInfo
                {
                    ProductInfoID = 26,
                    Description = "SSD NVMe PCIE 4.0 d'une capacité de 1 To dont 802 Go libres. La Xbox Series X propose une résolution 4K avec une fréquence d'affichage maximale de 120 images par seconde . La mémoire de la Xbox Series X n'est pas utilisée entièrement dans les jeux.",
                },
                new ProductInfo
                {
                    ProductInfoID = 27,
                    Description = "Star Ocean [a] est un jeu de rôle d'action de 1996développé par tri-Ace et publié par Enix pour la Super Famicom . Le premier jeu de la série Star Ocean , il est sorti uniquement au Japon en juillet 1996, et a été le premier jeu développé par tri-Ace , composé de membres du personnel qui avaient précédemment quitté Wolf Team en raison d'être mécontents du processus de développement de Tales of Phantasia avec Namco en 1995. Le jeu utilisait une puce de compression spécialedans sa cartouche pour compresser et stocker toutes les données du jeu grâce à des graphismes qui ont repoussé les limites de la Super Famicom. De plus, le jeu avait un doublage pour l'intro du jeu et des clips vocaux qui ont été joués pendant le gameplay de combat du jeu, une rareté pour les jeux sur le système.",
                },
                new ProductInfo
                {
                    ProductInfoID = 28,
                    Description = "Greak: Memories of Azur est un jeu solo en side-scrolling aux animations dessinées à la main. Vous y incarnez trois enfants nommés Greak, Adara et Raydel, que vous guiderez à travers les terres d'Azur. Contrôlez chacun d'eux à tour de rôle, et utilisez leurs compétences uniques pour échapper ....",
                },
                new ProductInfo
                {
                    ProductInfoID = 29,
                    Description = "Les Sims 4 est le jeu de simulation de vie très attendu qui vous permet de jouer avec la vie comme jamais auparavant. Créez de nouveaux Sims avec intelligence et émotion.",
                },
                new ProductInfo
                {
                    ProductInfoID = 30,
                    Description = "Le joueur joue le rôle d'un chasseur, tuant ou piégeant de grands monstres à travers divers paysages dans le cadre de quêtes qui leur sont confiées par les habitants , certaines quêtes impliquant la collecte d'un ou plusieurs objets, ce qui peut exposer le chasseur à divers risques. monstres.",
                });

            builder.Entity<Product>().HasData(
                new Product
                {
                    ProductID = 1,
                    Name = "Mario Odyssey",
                    Price = (float)59.99,
                    Quantity = 1,
                    ProductInfoID = 1,
                    StatusID = 1,
                    ActualPrice = (float)58.99,
                    PathImage = "/img/Produit/VideoGames/JeuxSwitch/JeuxSwitchMarioOdyssey.png",
                    PlateformeID = 1,
                    PublisherID = 1,
                    CompagnyID = 1,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 2,
                    Name = "The Legend of Zelda Link's Awakening",
                    Price = (float)59.99,
                    Quantity = 1,
                    ProductInfoID = 2,
                    StatusID = 1,
                    ActualPrice = (float)59.99,
                    PathImage = "/img/Produit/VideoGames/JeuxSwitch/JeuxSwitchLinksAwakening.png",
                    PlateformeID = 1,
                    PublisherID = 1,
                    CompagnyID = 1,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 3,
                    Name = "Mario 3D World",
                    Price = (float)79.99,
                    Quantity = 1,
                    ProductInfoID = 3,
                    StatusID = 1,
                    ActualPrice = (float)79.99,
                    PathImage = "/img/Produit/VideoGames/JeuxSwitch/JeuxSwitchMario3DWorld.png",
                    PlateformeID = 1,
                    PublisherID = 1,
                    CompagnyID = 1,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 4,
                    Name = "New Super Mario Bros U\r\nDeluxe",
                    Price = (float)79.99,
                    Quantity = 1,
                    ProductInfoID = 4,
                    StatusID = 1,
                    ActualPrice = (float)79.99,
                    PathImage = "/img/Produit/VideoGames/JeuxSwitch/JeuxSwitchMarioBrosDeluxe.png",
                    PlateformeID = 1,
                    PublisherID = 1,
                    CompagnyID = 1,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 5,
                    Name = "Mario Golf Super Rush",
                    Price = (float)79.99,
                    Quantity = 1,
                    ProductInfoID = 5,
                    StatusID = 1,
                    ActualPrice = (float)79.99,
                    PathImage = "/img/Produit/VideoGames/JeuxSwitch/JeuxSwitchMarioGolfSuperRush.png",
                    PlateformeID = 1,
                    PublisherID = 1,
                    CompagnyID = 1,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 6,
                    Name = "Mario Party Superstars",
                    Price = (float)79.99,
                    Quantity = 1,
                    ProductInfoID = 6,
                    StatusID = 1,
                    ActualPrice = (float)79.99,
                    PathImage = "/img/Produit/VideoGames/JeuxSwitch/JeuxSwitchMarioPartySuperstars.png",
                    PlateformeID = 1,
                    PublisherID = 1,
                    CompagnyID = 1,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 7,
                    Name = "Minecraft",
                    Price = (float)34.99,
                    Quantity = 1,
                    ProductInfoID = 7,
                    StatusID = 1,
                    ActualPrice = (float)34.99,
                    PathImage = "/img/Produit/VideoGames/JeuxXbox/JeuxXboxMinecraft.png",
                    PlateformeID = 2,
                    PublisherID = 2,
                    CompagnyID = 2,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 8,
                    Name = "State Of Decay 2",
                    Price = (float)49.99,
                    Quantity = 1,
                    ProductInfoID = 8,
                    StatusID = 1,
                    ActualPrice = (float)49.99,
                    PathImage = "/img/Produit/VideoGames/JeuxXbox/JeuxXboxStateOfDecay.png",
                    PlateformeID = 2,
                    PublisherID = 5,
                    CompagnyID = 5,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 9,
                    Name = "Rayman Legends",
                    Price = (float)44.99,
                    Quantity = 1,
                    ProductInfoID = 9,
                    StatusID = 1,
                    ActualPrice = (float)44.99,
                    PathImage = "/img/Produit/VideoGames/JeuxXbox/JeuxXboxRayman.png",
                    PlateformeID = 2,
                    PublisherID = 3,
                    CompagnyID = 3,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 10,
                    Name = "Assasin Creed\r\nBlack Flag",
                    Price = (float)39.99,
                    Quantity = 1,
                    ProductInfoID = 10,
                    StatusID = 1,
                    ActualPrice = (float)39.99,
                    PathImage = "/img/Produit/VideoGames/JeuxXbox/JeuxXboxAssasinCreed.png",
                    PlateformeID = 2,
                    PublisherID = 3,
                    CompagnyID = 3,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 11,
                    Name = "Steep",
                    Price = (float)59.99,
                    Quantity = 1,
                    ProductInfoID = 11,
                    StatusID = 1,
                    ActualPrice = (float)59.99,
                    PathImage = "/img/Produit/VideoGames/JeuxXbox/JeuxXboxSteep.png",
                    PlateformeID = 2,
                    PublisherID = 3,
                    CompagnyID = 3,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 12,
                    Name = "Mx GP 2",
                    Price = (float)49.99,
                    Quantity = 1,
                    ProductInfoID = 12,
                    StatusID = 1,
                    ActualPrice = (float)49.99,
                    PathImage = "/img/Produit/VideoGames/JeuxXbox/JeuxXboxMxgp.png",
                    PlateformeID = 2,
                    PublisherID = 6,
                    CompagnyID = 6,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 13,
                    Name = "Horizon\r\nForbiden West",
                    Price = (float)79.99,
                    Quantity = 1,
                    ProductInfoID = 13,
                    StatusID = 1,
                    ActualPrice = (float)79.99,
                    PathImage = "/img/Produit/VideoGames/JeuxPS4/JeuxPS4HorizonForbiden.png",
                    PlateformeID = 3,
                    PublisherID = 7,
                    CompagnyID = 7,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 14,
                    Name = "Star Wars Squadrons",
                    Price = (float)79.99,
                    Quantity = 1,
                    ProductInfoID = 14,
                    StatusID = 1,
                    ActualPrice = (float)79.99,
                    PathImage = "/img/Produit/VideoGames/JeuxPS4/JeuxPS4StarWarsSquadrons.png",
                    PlateformeID = 3,
                    PublisherID = 4,
                    CompagnyID = 4,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 15,
                    Name = "Ghost Recon\r\nWildlands",
                    Price = (float)59.99,
                    Quantity = 1,
                    ProductInfoID = 15,
                    StatusID = 1,
                    ActualPrice = (float)59.99,
                    PathImage = "/img/Produit/VideoGames/JeuxPS4/JeuxPS4UbisoftGostRacon.png",
                    PlateformeID = 3,
                    PublisherID = 3,
                    CompagnyID = 3,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 16,
                    Name = "Hot Wheels Unleashed",
                    Price = (float)59.99,
                    Quantity = 1,
                    ProductInfoID = 16,
                    StatusID = 1,
                    ActualPrice = (float)59.99,
                    PathImage = "/img/Produit/VideoGames/JeuxPS5/JeuxPS5HotWheelsUnleashed.png",
                    PlateformeID = 4,
                    PublisherID = 6,
                    CompagnyID = 6,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 17,
                    Name = "Rainbow Six Siege\r\nÉdition Deluxe",
                    Price = (float)79.99,
                    Quantity = 1,
                    ProductInfoID = 17,
                    StatusID = 1,
                    ActualPrice = (float)79.99,
                    PathImage = "/img/Produit/VideoGames/JeuxPS5/JeuxPS5RainbowSIXSiegeditionDeluxe.png",
                    PlateformeID = 4,
                    PublisherID = 3,
                    CompagnyID = 3,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 18,
                    Name = "Immortals Fenyx Rising",
                    Price = (float)79.99,
                    Quantity = 1,
                    ProductInfoID = 18,
                    StatusID = 1,
                    ActualPrice = (float)79.99,
                    PathImage = "/img/Produit/VideoGames/JeuxPS5/JeuxPS5Fenyx.png",
                    PlateformeID = 4,
                    PublisherID = 3,
                    CompagnyID = 3,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 19,
                    Name = "Ensemble Joycon Rouge",
                    Price = (float)89.99,
                    Quantity = 1,
                    ProductInfoID = 19,
                    StatusID = 1,
                    ActualPrice = (float)89.99,
                    PathImage = "/img/Produit/ManetteDeJeux/JoyconRougeNintendoSwitch.png",
                    PlateformeID = 1,
                    PublisherID = 1,
                    CompagnyID = 1,
                    CategoryID = 3
                },
                new Product
                {
                    ProductID = 20,
                    Name = "Pro-Controller Pokémon Édition Flambino",
                    Price = (float)69.99,
                    Quantity = 1,
                    ProductInfoID = 20,
                    StatusID = 1,
                    ActualPrice = (float)69.99,
                    PathImage = "/img/Produit/ManetteDeJeux/ManettePokemonNintendoSwitchPro.png",
                    PlateformeID = 1,
                    PublisherID = 1,
                    CompagnyID = 1,
                    CategoryID = 3
                },
                new Product
                {
                    ProductID = 21,
                    Name = "Dual Sense 5",
                    Price = (float)89.99,
                    Quantity = 1,
                    ProductInfoID = 21,
                    StatusID = 1,
                    ActualPrice = (float)89.99,
                    PathImage = "/img/Produit/ManetteDeJeux/ManettePS5.png",
                    PlateformeID = 4,
                    PublisherID = 8,
                    CompagnyID = 8,
                    CategoryID = 3
                },
                new Product
                {
                    ProductID = 22,
                    Name = "Elite Controller 2",
                    Price = (float)149.99,
                    Quantity = 1,
                    ProductInfoID = 22,
                    StatusID = 1,
                    ActualPrice = (float)149.99,
                    PathImage = "/img/Produit/ManetteDeJeux/ManetteXboxOneElite2.png",
                    PlateformeID = 2,
                    PublisherID = 2,
                    CompagnyID = 2,
                    CategoryID = 3
                },
                new Product
                {
                    ProductID = 23,
                    Name = "Nintendo Switch",
                    Price = (float)349.99,
                    Quantity = 1,
                    ProductInfoID = 23,
                    StatusID = 1,
                    ActualPrice = (float)349.99,
                    PathImage = "/img/Produit/ConsoleDeJeux/Nintendo-Switch.png",
                    PlateformeID = 1,
                    PublisherID = 1,
                    CompagnyID = 1,
                    CategoryID = 1
                },
                new Product
                {
                    ProductID = 24,
                    Name = "Nintendo Switch OLED",
                    Price = (float)449.99,
                    Quantity = 1,
                    ProductInfoID = 24,
                    StatusID = 1,
                    ActualPrice = (float)449.99,
                    PathImage = "/img/Produit/ConsoleDeJeux/NintendoSwitchOled.png",
                    PlateformeID = 1,
                    PublisherID = 1,
                    CompagnyID = 1,
                    CategoryID = 1
                },
                new Product
                {
                    ProductID = 25,
                    Name = "Playstation 5",
                    Price = (float)599.99,
                    Quantity = 1,
                    ProductInfoID = 25,
                    StatusID = 1,
                    ActualPrice = (float)599.99,
                    PathImage = "/img/Produit/ConsoleDeJeux/PS5DigitalEdition.png",
                    PlateformeID = 4,
                    PublisherID = 8,
                    CompagnyID = 8,
                    CategoryID = 1
                },
                new Product
                {
                    ProductID = 26,
                    Name = "Xbox Series X",
                    Price = (float)499.99,
                    Quantity = 1,
                    ProductInfoID = 26,
                    StatusID = 1,
                    ActualPrice = (float)499.99,
                    PathImage = "/img/Produit/ConsoleDeJeux/XboxSerieX.png",
                    PlateformeID = 2,
                    PublisherID = 2,
                    CompagnyID = 2,
                    CategoryID = 1
                },
                new Product
                {
                    ProductID = 27,
                    Name = "Star Ocean",
                    Price = (float)79.99,
                    Quantity = 1,
                    ProductInfoID = 27,
                    StatusID = 1,
                    ActualPrice = (float)79.99,
                    PathImage = "/img/Produit/VideoGames/JeuxPS4/JeuxPS4StarOcean.png",
                    PlateformeID = 3,
                    PublisherID = 9,
                    CompagnyID = 9,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 28,
                    Name = "Greak",
                    Price = (float)27.99,
                    Quantity = 1,
                    ProductInfoID = 28,
                    StatusID = 1,
                    ActualPrice = (float)27.99,
                    PathImage = "/img/Produit/VideoGames/JeuxPS5/JeuxPS5Greak.png",
                    PlateformeID = 4,
                    PublisherID = 10,
                    CompagnyID = 10,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 29,
                    Name = "Sims 4",
                    Price = (float)29.99,
                    Quantity = 1,
                    ProductInfoID = 29,
                    StatusID = 1,
                    ActualPrice = (float)29.99,
                    PathImage = "/img/Produit/VideoGames/JeuxPc/JeuxPcSims4.png",
                    PlateformeID = 5,
                    PublisherID = 4,
                    CompagnyID = 4,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 30,
                    Name = "Monster Hunter",
                    Price = (float)21.99,
                    Quantity = 1,
                    ProductInfoID = 30,
                    StatusID = 1,
                    ActualPrice = (float)21.99,
                    PathImage = "/img/Produit/VideoGames/JeuxPc/JeuxPcMonsterHunter.png",
                    PlateformeID = 5,
                    PublisherID = 11,
                    CompagnyID = 11,
                    CategoryID = 2
                });

            builder.Entity<Address>().HasData(
                new Address
                {
                    AddressID = 1,
                    Country = "Canada",
                    City = "St-Hyacinthe",
                    Province = "Qc",
                    StreetAddress = "123 rue 1234",
                    PostalCode = "A1B 2C3"
                });

            builder.Entity<Order>().HasData(
                new Order
                {
                    OrderID = 1,
                    IsConfirmed = true,
                    UserName = "admin@admin.com",
                    PriceNoTaxe = 150,
                    Taxe = 45,
                    PriceWithTaxe = 195,
                    Status = (int)OrderStatus.Refuser,
                    OrderDate = DateTime.MinValue,
                    VerifiedDate = DateTime.MinValue,
                    AddressID = 1,
                    EmailAddress = "admin@admin.com"
                },
                new Order
                {
                    OrderID = 2,
                    IsConfirmed = true,
                    UserName = "admin@admin.com",
                    PriceNoTaxe = 1000,
                    Taxe = 150,
                    PriceWithTaxe = 1150,
                    Status = (int)OrderStatus.EnAttente,
                    OrderDate = DateTime.MinValue,
                    VerifiedDate = DateTime.MinValue,
                    AddressID = 1,
                    EmailAddress = "admin@admin.com"
                },
                new Order
                {
                    OrderID = 3,
                    IsConfirmed = true,
                    UserName = "admin@admin.com",
                    PriceNoTaxe = 60,
                    Taxe = 15,
                    PriceWithTaxe = 75,
                    Status = (int)OrderStatus.Accepter,
                    OrderDate = DateTime.MinValue,
                    VerifiedDate = DateTime.MinValue,
                    AddressID = 1,
                    EmailAddress = "admin@admin.com"
                },
                new Order
                {
                    OrderID = 4,
                    IsConfirmed = true,
                    UserName = "admin@admin.com",
                    PriceNoTaxe = 70,
                    Taxe = 25,
                    PriceWithTaxe = 95,
                    Status = (int)OrderStatus.Livraison,
                    OrderDate = DateTime.MinValue,
                    VerifiedDate = DateTime.MinValue,
                    AddressID = 1,
                    EmailAddress = "admin@admin.com"
                },
                new Order
                {
                    OrderID = 5,
                    IsConfirmed = true,
                    UserName = "admin@admin.com",
                    PriceNoTaxe = 240,
                    Taxe = 45,
                    PriceWithTaxe = 285,
                    Status = (int)OrderStatus.Refuser,
                    OrderDate = DateTime.MinValue,
                    VerifiedDate = DateTime.MinValue,
                    AddressID = 1,
                    EmailAddress = "admin@admin.com"
                });

            builder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    ProductId = 1,
                    OrderId = 1,
                    OrderItemId = 1,
                    OrderItemName = "admin@admin.com",
                    Quantity = 2
                },
                new OrderItem
                {
                    ProductId = 10,
                    OrderId = 2,
                    OrderItemId = 2,
                    OrderItemName = "admin@admin.com",
                    Quantity = 7
                },
                new OrderItem
                {
                    ProductId = 11,
                    OrderId = 3,
                    OrderItemId = 3,
                    OrderItemName = "admin@admin.com",
                    Quantity = 1
                },
                new OrderItem
                {
                    ProductId = 20,
                    OrderId = 4,
                    OrderItemId = 4,
                    OrderItemName = "admin@admin.com",
                    Quantity = 1
                },
                new OrderItem
                {
                    ProductId = 5,
                    OrderId = 5,
                    OrderItemId = 5,
                    OrderItemName = "admin@admin.com",
                    Quantity = 1
                },
                new OrderItem
                {
                    ProductId = 1,
                    OrderId = 5,
                    OrderItemId = 6,
                    OrderItemName = "admin@admin.com",
                    Quantity = 2
                },
                new OrderItem
                {
                    ProductId = 16,
                    OrderId = 5,
                    OrderItemId = 7,
                    OrderItemName = "admin@admin.com",
                    Quantity = 1
                });
        }
    }
}
