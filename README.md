# Hunter-Assassin-project Unity 3D

## Introduction
Bienvenue dans le référentiel GitHub du remake de Hunter Assassin sur Unity 3D. Ce projet vise à recréer le célèbre jeu Hunter Assassin en utilisant le moteur de jeu Unity 3D et le langage de programmation C#. Le projet comprend des fonctionnalités telles que le déplacement au clic, l'IA des ennemis, ainsi que des améliorations sonores et visuelles pour une expérience de jeu enrichie.

## Outils 
- **Unity 2020.3** : Comme moteur de jeu.
- **Visual Studio 2022** : Comme éditeur de code en C#.
- **Blender** : Pour la modélisation des décors et personnages. 

## Fonctionnalités
### Fonctionnalités Initiales
- **Déplacement au Clic** : Le joueur peut cliquer sur n'importe quel point du terrain, et l'avatar se dirige automatiquement vers cette destination en utilisant le composant NavMesh Agent de Unity.
- **IA des Ennemis** : Les ennemis patrouillent de manière aléatoire et poursuivent le joueur en lui tirant dessus lorsqu'il entre dans leur champ de détection.
- **Conditions de Victoire et de Défaite** : Le joueur doit éliminer tous les ennemis sans se faire toucher. La partie est gagnée lorsque tous les ennemis sont éliminés et perdue si le joueur est touché ou si le temps imparti s'écoule.
- **Compteur de Temps** : Un chronomètre limite la durée de chaque niveau, ajoutant un élément de défi supplémentaire.

### Nouvelles Améliorations
- **Textures** : Ajout de textures (Sprite 2D and UI) pour le sol et les murs dans un style de pierre avec de la mousse, améliorant l'esthétique visuelle du jeu.
- **Interfaces Utilisateur** : Amélioration des interfaces avec des arrière-plans et des boutons plus attractifs pour une meilleure expérience utilisateur.
- **Effets Sonores** : Intégration de sons pour l'élimination des ennemis, les tirs et les clics sur les boutons en utilisant les composants Audio Source et Audio Clip de Unity.
- **Système de Sauvegarde** : Implémentation d'un système de sauvegarde avec PlayerPrefs permettant de conserver le dernier niveau atteint et la somme d'argent collectée même après la fermeture du jeu.

## Installation
Pour cloner et exécuter ce projet localement, suivez ces étapes :

1. **Clonez le référentiel** :
   ```bash
   git clone https://github.com/votre_nom_utilisateur/hunter-assassin-remake.git

