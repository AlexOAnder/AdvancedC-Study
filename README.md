# Advanced C# Study Repository

Репозиторий для хранения практических задач по С#

Содержание (обновляемое): 

# Struct
* Создать правильную реализацию струrтуры с точки зрения ValueType. Переопределить GetHashCode для использования в HashTable и Equals для сравнений.

# BCL lesson:
BCL6: Необходимо создать приложение, которое будет считывать текстовые данные из некоторых файлов и проводить их обработку. Требуемая культура для интерфейса, а также список расширений файлов, обрабатываемых программой, должны храниться в файле конфигураций.

Обработка файлов включает в себя следующее:
    
Выводить на экран информацию о файле. Например, {file_name} | {file_ext} | {size}. Формат для информации о файле также следует брать из файла конфигурации.
    
С использованием регулярных выражений проверять в тексте наличие русских символов и, если таковые будут, отобразить на экране каждую подстроку с указанием номера строки и номера символа. Например, Ln: {line} Col: {col} Text: {text}. Аналогично первому пункту, формат о найденных подстроках должен извлекаться из файла конфигураций
    
В случае, если встреченную подстроку можно изменить на соответствующую английскую, заменить ее и информацию об этом отобразить в строке вывода, описанной выше.

В конце работы с файлом указать количество найденных русских подстрок и количество возможных исправлений.

Спросить у пользователя разрешение на сохранение измененных текстовых данных. Если пользователь подтверждает сохранение: сохранить новые данные под оригинальным именем, а старый файл сохранить переместить в резервную папку.
    
Файл конфигураций должен содержать механизм включения/отключения запроса пользователю на сохранение файла. Если возможность запроса отключена, файл сохраняется автоматически.
    
Сохранение оригинальных документов в резервную папку, а также путь к этой папке, также должен регулироваться через механизм конфигураций
    
Структура пользовательской секции в файле конфигураций может выглядеть так:

    <scanSettings culture="<interface culture>"> 
        <targetFiles>
            <file extension="<file extension #1>"/>
            <file extension="<file extension #2>"/>
        </targetFile>
        <logging>
            <logger fileInfoFormat="<logging format>" entryInfoFormat="<logging format>"/>
        </logging>
        <saving saveConfirmation="<true/false>">
            <backup file="<backup destination>"/>
        </saving>
    </scanSettings>

+ BSL1: Напишите атрибут, который можно применить только к классу. Атрибут содержит информацию об авторе кода (имя, e-mail). Напишите функционал, который позволит вывести эту информацию в консоль.
+ BCL2: Напишите метод для сортировки массива строк в независимости от региональных стандартов пользователя. Использование Linq запрещено.
+ BCL3: Напишите метод для конкатенации каждого второго элемента массива строк в результирующую строку. Обоснуйте выбор реализации. Использование Linq запрещено.
+ BCL4: Приведите пример использования Nullable типа при проектировании класса. Класс не используется для работы с записями БД и не используется для их представления. Обоснуйте свой выбор.

##########################################################

# Exception Handling

* Напишите консольное приложение, которое выводит на экран первый из введенных символов каждой строки ввода. Опишите корректное поведение приложения, если пользователь ввел пустую строку.
