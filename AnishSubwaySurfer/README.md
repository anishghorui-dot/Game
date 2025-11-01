# ANISH: Subway Surfer Clone
## Created by ANISH

A complete Subway Surfer-style endless runner game built with **Unity and C#** for optimal Android performance!

---

## 🎮 Features

### Core Gameplay
- **Endless Runner** - Run forever, dodge obstacles, collect coins!
- **3-Lane System** - Swipe left/right to change lanes
- **Jump & Slide** - Swipe up to jump, down to slide under obstacles
- **Smooth Controls** - Touch swipe controls + keyboard support

### ANISH Branding
- **Collect A-N-I-S-H Letters** - Spell your name for 500 bonus coins!
- **Name Display** - Shows collected letters and bonuses
- **Personalized UI** - Your name featured throughout

### Power-Ups
- 🧲 **Magnet** - Attracts nearby coins automatically
- ⚡ **Speed Boost** - Increase running speed
- 🛡️ **Shield** - Invincibility for a short time
- 💰 **Double Coins** - 2x coin value temporarily

### Advanced Features
- **Dynamic Environment Generation** - Infinite procedural world
- **Coin Collection System** - Gather coins in lanes
- **Score System** - Distance + coins = high score
- **Buildings & Decorations** - Cyberpunk city environment
- **Smooth Camera Follow** - Professional third-person camera

---

## 📁 Project Structure

```
AnishSubwaySurfer/
├── Assets/
│   ├── Scripts/
│   │   ├── PlayerController.cs       # Player movement & input
│   │   ├── GameManager.cs            # UI, scoring, game state
│   │   ├── EnvironmentGenerator.cs   # Procedural world generation
│   │   ├── CameraFollow.cs           # Smooth camera tracking
│   │   ├── LetterCollectible.cs      # ANISH letter behavior
│   │   └── PowerUp.cs                # Power-up effects
│   ├── Scenes/
│   │   └── MainGame.unity            # Main game scene
│   └── Prefabs/                      # Reusable game objects
└── README.md
```

---

## 🛠️ Setup Instructions

### Option 1: Open in Unity (Recommended)

1. **Install Unity Hub** - Download from https://unity.com/download
2. **Install Unity Editor** - Version 2021.3 LTS or newer recommended
3. **Open Project**:
   ```
   Unity Hub → Projects → Add → Select "AnishSubwaySurfer" folder
   ```
4. **Open Main Scene**: `Assets/Scenes/MainGame.unity`
5. **Press Play** to test in Unity Editor!

### Option 2: Build for Android

1. **Open Project in Unity**
2. **Install Android Build Support**:
   - Unity Hub → Installs → Your Unity Version → Add Modules
   - Check "Android Build Support" (includes Android SDK & NDK)
3. **Switch Platform**:
   - File → Build Settings → Android → Switch Platform
4. **Build Settings**:
   - Add Scene: `Assets/Scenes/MainGame.unity`
   - Set Bundle Identifier: `com.anish.subwaysurfer`
5. **Player Settings** (Edit → Project Settings → Player):
   - Company Name: `ANISH`
   - Product Name: `ANISH Subway Surfer`
   - Icon: Upload custom icon (optional)
   - Minimum API Level: 24 (Android 7.0)
6. **Build APK**:
   - File → Build Settings → Build
   - Choose output location
   - Wait for build to complete
7. **Install on Android**: Transfer APK to phone and install!

---

## 🎯 How to Play

### Keyboard Controls (PC Testing)
- **A / Left Arrow** - Move Left
- **D / Right Arrow** - Move Right
- **W / Up Arrow / Space** - Jump
- **S / Down Arrow** - Slide

### Touch Controls (Mobile)
- **Swipe Left** - Move to left lane
- **Swipe Right** - Move to right lane
- **Swipe Up** - Jump over obstacles
- **Swipe Down** - Slide under barriers

### Objectives
1. **Run as far as possible** - Avoid obstacles!
2. **Collect coins** - Increase your score
3. **Collect A-N-I-S-H letters** - Get 500 bonus coins per set!
4. **Grab power-ups** - Magnet, Shield, Speed Boost
5. **Beat your high score** - Challenge yourself!

---

## 🎨 Customization

### Change Player Speed
Edit `PlayerController.cs`:
```csharp
public float forwardSpeed = 10f; // Increase for faster gameplay
```

### Adjust Spawn Rates
Edit `EnvironmentGenerator.cs`:
```csharp
public float obstacleSpawnChance = 0.4f; // 0-1 (higher = more obstacles)
public float coinSpawnChance = 0.6f;     // 0-1 (higher = more coins)
```

### Modify Letter Bonus
Edit `GameManager.cs`:
```csharp
public int letterBonus = 500; // Change bonus amount
```

---

## 🚀 Performance Tips

### For Smooth Android Performance:
1. **Use Low Poly Models** - Keep meshes under 500 triangles
2. **Optimize Materials** - Use Mobile/Diffuse shaders
3. **Object Pooling** - Reuse coins/obstacles instead of spawning new ones
4. **Bake Lighting** - Pre-calculate shadows and lighting
5. **Target 60 FPS** - Test on actual Android device

### Unity Optimization Settings:
- **Quality Settings** (Edit → Project Settings → Quality):
  - Set "Medium" quality for Android
  - Disable shadows on mobile builds
  - Reduce texture quality
- **Graphics Settings**:
  - Use "Linear" color space for better performance
  - Enable "GPU Skinning"

---

## 📦 Building for Production

### Create Release APK:
1. **File → Build Settings → Android**
2. **Player Settings**:
   - Set "Scripting Backend" to IL2CPP (better performance)
   - Set "Target Architectures" to ARM64 (required for Google Play)
3. **Build Settings**:
   - Check "Build App Bundle (Google Play)" for Play Store
   - OR uncheck for standalone APK
4. **Keystore** (for signing):
   - Player Settings → Publishing Settings
   - Create new keystore for release builds
5. **Build**!

---

## 🎓 Learning Resources

### Unity Tutorials:
- Official Unity Learn: https://learn.unity.com/
- Endless Runner Tutorial: https://learn.unity.com/project/endless-runner
- C# Basics: https://learn.unity.com/course/beginner-scripting

### C# Programming:
- Microsoft C# Guide: https://docs.microsoft.com/en-us/dotnet/csharp/
- Unity Scripting Reference: https://docs.unity3d.com/ScriptReference/

---

## 🐛 Troubleshooting

**Problem**: Unity won't open project
- **Solution**: Make sure Unity version is 2021.3 or newer

**Problem**: Build fails on Android
- **Solution**: Install Android Build Support via Unity Hub

**Problem**: Game is laggy on phone
- **Solution**: Lower quality settings, reduce spawn rates, optimize models

**Problem**: Touch controls don't work
- **Solution**: Make sure you're building for Android (not testing in PC editor)

---

## 🌟 Future Enhancements

Want to make it even better? Add:
- [ ] Character customization (different runners)
- [ ] Multiple environments (city, subway, desert)
- [ ] Daily challenges
- [ ] Leaderboards (online scores)
- [ ] More power-ups (jetpack, super jump)
- [ ] Sound effects and music
- [ ] Particle effects for collections
- [ ] Mission system
- [ ] In-app purchases (optional)

---

## 📜 Credits

**Created by ANISH**
- Game Design: ANISH
- C# Programming: ANISH
- Unity Development: ANISH

Inspired by Subway Surfers (Kiloo & SYBO Games)

---

## 📄 License

This is a personal learning project. Feel free to modify and learn from the code!

**Note**: If publishing to app stores, create original assets (don't copy Subway Surfers assets).

---

## 🎉 Enjoy Your Game!

You now have a complete Subway Surfer-style game using the same technology (Unity + C#) as the original! 

**Next Steps**:
1. Open in Unity
2. Test in editor
3. Build for Android
4. Show off your name "ANISH" to everyone! 🚀

---

Made with ❤️ by ANISH
