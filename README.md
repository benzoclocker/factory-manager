# Factory Manager

### Test task for "Saint.wtf"

### Unity version: 2020.3.22f1

### Запускать со сцены "Initial"

### Task
    Есть три здания, которые производят три вида ресурсов.
    У зданий есть два склада для потребляемых и производимых
    ресурсов, которые имеют ограниченную вместимость.
    Ресурсы имеют вид прямоугольных блоков трех разных цветов.

    Схема производства изображена на рисунке:

    - первое здание производит ресурс №1 за единицу времени.
    - второе для производства требует на складе ресурс №1.
    - третье соответственно требует ресурсы №1 и №2.

	Здания прекращают производство  в двух случаях:
    - нет необходимых ресурсов на входящем складе
    - заполненность исходящего склада

    С помощью текста в UI нужно сообщить игроку,
    какое производство и по какой причине остановлено.
	Игрок управляет с помощью виртуального джойстика персонажем
    (капсуль). Персонаж имеет инвентарь для переноски
    ресурсов, с ограниченной вместимостью.
    Ресурсы в инвентаре отображаются в виде стека за
    спиной у персонажа. Ресурсы можно собирать на складе
    для произведенных ресурсов, и передавать на склад 
    для потребляемых, при пересечении триггерной зоны склада.
    Процесс передачи каждого отдельной единицы ресурса 
    занимает определенное время.

	Все перемещения ресурсов:
    здание -> склад,
    склад -> персонаж,
    персонаж -> склад,
    склад -> здание
    ! должны быть визуализированы линейной интерполяцией.

