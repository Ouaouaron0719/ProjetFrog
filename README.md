# Projet Frooooog
## Description du projet

Ce projet est un jeu de plateforme 2D dont le gameplay principal repose sur le déverrouillage de capacités (Dash, Wall Slide, Wall Jump). Le joueur progresse en acquérant progressivement ces capacités afin d’explorer les niveaux.

Les premiers niveaux sont conçus comme des niveaux d’apprentissage. Ils permettent de guider le joueur dans la compréhension des mécaniques, tout en mettant l’accent sur l’intégration des différentes capacités. L’objectif est que le joueur puisse ensuite les combiner efficacement dans les niveaux suivants.

## Architecture du système

Le projet est structuré en plusieurs modules :

- Player (contrôle du joueur et machine à états)
- Ability System (gestion des capacités)
- UI (Main Menu, Pause Panel, système de messages)
- Database (stockage SQLite)
- Trigger System (Hint, Scene Transition, Respawn)
- Camera (suivi et effets de shake)

Chaque module est relativement indépendant et communique avec les autres à travers des événements ou des interfaces, ce qui rend le projet plus clair et extensible.

## Design Patterns (élément principal)
### State Pattern (système central)

La machine à états est l’élément le plus important du projet, principalement implémenté dans le script Player.

Tous les comportements du joueur (déplacement, saut, Dash, Wall Slide, Wall Jump) sont gérés par différents états. Chaque état hérite de EntityState, et la transition entre les états est contrôlée par les entrées du joueur et les conditions liées aux capacités.

Par exemple :

- Le Dash ne peut être utilisé que si CanUseDash() est vrai
- Le Wall Jump dépend de la détection du mur
- L’accès aux capacités est déterminé par les données stockées dans DatabaseManager

Cette approche permet de structurer la logique du joueur, d’éviter des conditions complexes et de faciliter l’ajout de nouvelles capacités.

### Observer Pattern (communication entre systèmes)

Le projet utilise GameEvents pour implémenter le pattern Observer, principalement pour le système de messages UI.

- HintTrigger envoie un message lorsqu’un joueur entre dans une zone
- GameEvents agit comme un intermédiaire
- UIMessageManager reçoit le message et l’affiche à l’écran

Le flux est donc :

HintTrigger → GameEvents → UIMessageManager

Cela permet de découpler le gameplay et l’interface utilisateur, et rend le système plus flexible et maintenable.

### Singleton Pattern (gestion globale)

Les classes PauseManager et DatabaseManager utilisent le pattern Singleton.

Chaque classe possède une instance statique (Instance) afin de garantir qu’il n’y a qu’une seule instance active dans la scène.

- PauseManager gère la pause (ESC), le Time.timeScale et l’affichage du menu pause
- DatabaseManager gère la connexion SQLite et les opérations de sauvegarde/chargement des capacités

Ce pattern permet un accès global et assure la cohérence des données.

## Composition (architecture Unity)

Le projet repose sur le modèle de composition de Unity.

Le Player est composé de plusieurs composants (états, mouvement, capacités)
Les triggers (HintTrigger, RespawnTrigger, SceneTransitionTrigger) sont des composants indépendants
La caméra est séparée en CameraFollow et CameraShake

Cette approche permet de réduire le couplage, d’augmenter la réutilisabilité et de simplifier la maintenance.

## Base de données (SQLite)

Le projet utilise SQLite via DatabaseManager pour stocker les capacités débloquées.

Chaque capacité est enregistrée lors de son acquisition et peut être rechargée lors d’un changement de scène. Il est également possible de réinitialiser les capacités, notamment pour les niveaux d’apprentissage.

Ce système simule un mécanisme de sauvegarde similaire à celui d’un jeu réel.

## Tests et corrections

Plusieurs tests ont été réalisés au cours du développement. Certains problèmes ont été identifiés et corrigés, notamment :

Le cooldown du Dash qui ne fonctionnait pas correctement
Un problème de Camera Shake qui restait actif en continu
La non-mise à jour des capacités après un changement de scène
L’amélioration de l’affichage des messages UI (temps basé sur la longueur du texte)

Ces corrections montrent une démarche de test et d’amélioration continue.
