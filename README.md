# Advanced C# <h1>Task 1</h1> <p>Создайте класс FileSystemVisitor, который позволяет обходить дерево папок в файловой системе, начиная с указанной точки. Указанный класс должен:</p> <ul><li>Возвращать все найденные файлы и папки в виде линейной последовательности, для чего реализовать свой итератор (по возможности используя оператор yield)</li> <li>Давать возможность задать алгоритм фильтрации найденных файлов и папок в момент создания экземпляра FileSystemVisitor (через специальный перегруженный конструктор). Алгоритм должен задаваться в виде делегата/лямбды </li> <li>Генерировать уведомления (через механизм событий) о этапах своей работы. В частности, должны быть реализованы следующие события <ul><li>Start и Finish (для начала и конца поиска) </li> <li>FileFinded/DirectoryFinded для всех найденных файлов и папок до фильтрации, и FilteredFileFinded/filteredDirectoryFinded для файлов и папок прошедших фильтрацию. Данные события должны позволять (через установку специальных флагов в переданных параметрах): </li><ul> <li> прервать поиск </li> <li> исключить файлы/папки из конечного списка</li></ul></ul></li> </ul> <p>Напишите библиотеку тестов, демонстрирующих различные режимы работы FileSystemVisitor. Необходимость использования моков (mock) согласуйте с ментором.</p>
