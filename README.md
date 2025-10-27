# 🧮 ProCalculate Test Project

## 📋 Overview
**ProCalculate** — тестовое задание, реализующее модульный калькулятор с историей вычислений и обработкой ошибок.  
Архитектура следует принципам **Clean Architecture**, использует **MVP (Model–View–Presenter)** и **Zenject SignalBus** для событийной коммуникации между модулями.

---

## 🧱 Архитектура проекта

Проект состоит из **двух модулей (assembly)**:

| Модуль | Назначение |
|---------|-------------|
| **ProCalculate.Calculator** | Основная логика калькулятора, хранение истории, вычисления, UI и Presenter |
| **ProCalculate.Dialog** | Модуль диалоговых окон (popup сообщений об ошибках) |

Каждый модуль — отдельная `.asmdef`, подключаемая независимо.  
Это обеспечивает изоляцию кода и возможность переиспользовать компоненты в других проектах.

---

## 🧩 Clean Architecture слои

**1. Core (Domain layer)**  
Интерфейсы, модели и базовая бизнес-логика:
- `ICalculationService`, `IStorageService`, `ILogger`
- `StorageState`
- `ExpressionParser`

**2. Services (Data layer)**  
Реализации сервисов и работа с хранилищем:
- `CalculationService`
- `StorageService` (PlayerPrefs)
- `UnityLogger`

**3. UI (Presentation layer)**  
Реализация пользовательского интерфейса (View + Presenter):
- `CalculatorView` (ввод, кнопка, история, ошибки)
- `UIHistoryEntry`
- `ErrorPopupView`

**4. Presenters (Application layer)**  
Презентеры, связывающие View и бизнес-логику:
- `CalculatorPresenter`
- `ErrorPopupPresenter`

**5. Installers (Composition Root)**  
Zenject-конфигурация:
- `ProjectInstaller` — регистрация зависимостей, сигналов и связей.

---

## ⚡ Используемые технологии

- **Unity 2022.3 LTS**
- **TextMeshPro** — для отображения текста.
- **Zenject** — Dependency Injection и SignalBus.
- **MVP Pattern** — View пассивный, логика в Presenter.
- **Clean Architecture** — разделение по слоям ответственности.
- **Scriptable Assemblies (.asmdef)** — модульность и изоляция.

---

## 🧠 MVP структура
- **View** отвечает только за визуализацию (`CalculatorView`, `ErrorPopupView`).
- **Presenter** подписывается на события из View и SignalBus, управляет бизнес-логикой.
- **Model/Service** реализует вычисления, сохранение и выдачу данных.

---

## 📦 Содержимое

```
Assets/
 ├── Scripts/
 │   ├── CalculatorModule/
 │   │   ├── Runtime/
 │   │   │   ├── Core/...
 │   │   │   ├── Services/...
 │   │   │   ├── UI/Views/...
 │   │   │   └── Presenters/...
 │   │   └── CalculatorModule.asmdef
 │   ├── DialogModule/
 │   │   ├── Runtime/
 │   │   │   ├── Signals/
 │   │   │   ├── UI/
 │   │   │   └── Presenters/
 │   │   └── DialogModule.asmdef
 │   └── Installers/
 │       └── ProjectInstaller.cs
 ├── Prefabs/
 │    ├── Calculator/
 │    │    ├── CalculatorView.prefab
 │    │    └── UIHistoryEntry.prefab
 │    ├── ErrorDialog/
 │    │     └── ErrorPopupView.prefab 
 │    └──SceneContext.prefab
 ├─ Scenes/
 │    └── CalculatorScene.unity   
 └─ Resources/
      └── ProjectContext.prefab
 
   ```
---
## ▶️ Как запустить

1. Открой проект в **Unity 2022.3**.
2. Убедись, что установлены пакеты:
   - **TextMeshPro**
   - **Zenject**  
3. Открой сцену:  
   ```
   Assets/Scenes/CalculatorScene.unity
   ```
4. Нажми **Play**.

🧩 В сцене уже всё привязано:
- Вводи выражения типа `2+2`, `100+500`.
- Результаты появляются в истории (до 6 элементов, ScrollView автопрокручивается).
- При ошибке (например, `2+`, `abc`) появляется popup с сообщением.

---

## ⚙️ Примечания
- ProjectInstaller является EnterPoint проекта
- История и последнее выражение сохраняются через `PlayerPrefs`.
- В случае переполнения (слишком большие числа) — показывается сообщение об ошибке.
- Архитектура позволяет легко заменить UI (например, консоль или WebView) без изменения логики.
- Все классы следуют соглашениям о кодстайле: `PascalCase` для публичных, `_lowerCamelCase` для приватных `[SerializeField]`.

---

## 🧾 License
Проект предназначен исключительно для тестового задания.  
Все материалы можно свободно использовать в демонстрационных целях.
