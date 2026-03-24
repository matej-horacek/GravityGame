# Gravity Game (Pracovní název)
Tento projekt je 3D hra vytvořená v herním enginu Unity na styl klasického "bullet hell". Hlavní mechanikou hry je dynamická změna gravitace, která se přizpůsobuje povrchům, se kterými hráč koliduje. Hra dále obsahuje systém zdraví, pohyb hráče založený na fyzice, uživatelské rozhraní (UI Toolkit) a nepřátelskou umělou inteligenci využívající NavMesh a zorné pole (Field of View).

Hlavním konceptem je vytvořit střílečku kde se hráč a nepřátelé pohybují ve 3d prostoru, který umožňuje unikátní práci s gravitací( Změna směru, velikosti). Někteří nepřátelé a objekty ignorují gravitaci, někteří ne, stejně jako schopnosti hlavní postavy. 

## 🌟 Hlavní funkce
* **Dynamická gravitace:** Gravitace se mění podle povrchu, kterého se hráč dotkne, což umožňuje chůzi po zdech a stropech.
* **Fyzikální pohyb:** Hráč se pohybuje a skáče pomocí `Rigidbody` fyziky a Unity Input Systemu.
* **Systém zdraví a UI:** Hráč má implementované zdraví propojené s vizuálním Health Barem vytvořeným pomocí UI Toolkit.
* **Umělá inteligence:** Nepřátelé využívají zorné pole (úhly pohledu) k detekci hráče a `NavMeshAgent` k jeho pronásledování.
* **Sledování kamerou:** Momentálně se ve hře nachází 2 kamery: First person kamera(pohled z první osoby), která umožnuje otáčení pomocí pohybu myši a rozhlížení se do všech stran. Dále je zde third person camera která umožňuje větší přehled ale na druhou stranu tvoří jiné slepé body. Do budoucna se plánuje přidání 3. Free lock kamery která nebude přímo následovat hráče ale bude spíše statická.

## 🛠️ Technologie a závislosti
* **Engine:** Unity (skripty využívají `UnityEngine` a `Unity.Cinemachine`)
* **Vstupy:** Unity Input System (`UnityEngine.InputSystem`)
* **UI:** UI Toolkit (`UnityEngine.UIElements`)
* **AI:** NavMesh (`UnityEngine.AI`)
