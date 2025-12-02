# Survivor Game Demo (幸存者游戏 Demo)

这是一个基于 Unity 引擎开发的“吸血鬼幸存者（Vampire Survivors）”类 Roguelite 游戏 Demo。项目展示了完整的核心战斗循环、敌人生成、角色升级、技能系统以及基于 XLua 的热更新框架集成。

## 🎮 项目简介

该项目实现了一个通过不断击败敌人、收集经验宝石、升级并选择强化项来生存的游戏原型。代码架构采用了多种设计模式（如状态机、命令模式、对象池等），结构清晰。

## ✨ 核心功能

### 1\. 核心玩法

  * **武器系统**：
      * **自动武器**：如枪械（`FirearmsWeapon`）、爆炸（`Bomb`）。
      * **近战武器**：如剑（`Sword`）。
      * **升级系统**：支持攻击力、攻速、范围、生命值等属性的升级（`UpgradeObject`）。
  * **敌人系统**：
      * 多种敌人类型（普通敌人、魔法敌人、冲刺敌人）。
      * 基于波次（Wave）的生成逻辑（`EnemySpawner`, `EnemyWaveData`）。
      * 简单的 AI 行为（追逐、攻击）。
  * **经验与等级**：
      * 敌人掉落经验宝石（`ExperienceGem`）。
      * 角色拾取后升级，触发随机强化选项（`LevelUpFinished`）。

### 2\. 技术架构

  * **有限状态机 (FSM)**：
      * 用于管理玩家状态（Idle, Walk, Attack, Hit, Died）。
      * 用于管理敌人状态（Patrol, Chase, Attack, Hit）。
      * 详见 `Assets/Scripts/StateMachine` 和 `Assets/Scripts/Core`。
  * **事件驱动系统 (Event System)**：
      * 基于 `ScriptableObject` 的事件架构，实现模块解耦。
      * 支持多种参数类型的事件（Void, Int, Float, GameObject 等）。
      * 详见 `Assets/Scripts/Events`。
  * **对象池 (Object Pooling)**：
      * 高效管理敌人、子弹、掉落物、伤害数字的生成与回收。
      * 详见 `Assets/Scripts/ObjectPool`。

### 3\. 资源管理与热更

  * **Addressables**：使用 Unity Addressables 进行资源加载管理（`AAMgr`）。
  * **XLua 集成**：
      * 集成了 XLua 框架，支持 Lua 脚本编写和 C\# 热修复。

## 📂 目录结构说明

```
Assets/
├── Art/                # 美术资源
├── Scripts/            # 核心代码逻辑
│   ├── Attack/         # 武器与攻击逻辑
│   ├── CommandPattern/ # 输入命令模式实现
│   ├── Core/           # 核心实体类 (Player, Enemy, FSM)
│   ├── Enemy/          # 敌人生成与波次数据
│   ├── Events/         # SO 事件系统
│   ├── Experience/     # 经验值与宝石逻辑
│   ├── GameData/       # 数据保存与加载
│   ├── ObjectPool/     # 对象池管理器
│   ├── ResRelative/    # 资源加载 (Addressables, LuaMgr)
│   ├── ScriptableObject/# 游戏配置数据 (武器参数, 敌人数据)
│   ├── UI/             # UI 逻辑
│   └── ...
├── Scenes/             # 游戏场景 
├── XLua/               # XLua 框架核心文件
├── Lua/                # Lua 业务脚本
└── Settings/           # 渲染管线与项目设置
```



-----

*Copyright (c) 2025 LeiJay*
