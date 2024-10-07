using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KitchenLayout.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int Width { get; set; }

        [BindProperty]
        public int Depth { get; set; }

        [BindProperty]
        public float SinkX { get; set; }

        [BindProperty]
        public float SinkY { get; set; }

        [BindProperty]
        public int SinkWidth { get; set; }

        [BindProperty]
        public int SinkDepth { get; set; }

        [BindProperty]
        public bool SinkIsCorner { get; set; }

        [BindProperty]
        public int StoveWidth { get; set; }

        [BindProperty]
        public int StoveDepth { get; set; }

        [BindProperty]
        public bool StoveIsCorner { get; set; }

        public KitchenRoom Room { get; set; }

        public IActionResult OnPost()
        {
            Room = new KitchenRoom
            {
                Width = Width * 10,
                Depth = Depth * 10,
                Sink = new KitchenElement
                {
                    Width = SinkWidth * 10,
                    Depth = SinkDepth * 10,
                    IsCorner = SinkIsCorner
                },
                Stove = new KitchenElement
                {
                    Width = StoveWidth * 10,
                    Depth = StoveDepth * 10,
                    IsCorner = StoveIsCorner
                }
            };

            // Генерируем кухню
            GenerateKitchen(Room, (SinkX * 100, SinkY * 100));

            return Page();
        }

        const float offset = 10; // 0.5 метра

        public class KitchenRoom
        {
            public float Width { get; set; }
            public float Depth { get; set; }
            public KitchenElement Sink { get; set; }
            public KitchenElement Stove { get; set; }
            public string ErrorMessage { get; set; }
            public bool IsSuccessful { get; set; }
        }

        public class KitchenElement
        {
            public float Width { get; set; }
            public float Depth { get; set; }
            public (float X, float Y) Position { get; set; }
            public (float X, float Y) PipePosition { get; set; }
            public bool IsCorner { get; set; }
        }

        private void GenerateKitchen(KitchenRoom room, (float X, float Y) sinkCoordinates)
        {
            // Определяем минимальные отступы в пикселях
            const int offset = 10; // 10 сантиметров в пикселях

            // Преобразуем координаты трубы (в метрах) в пиксели
            float sinkX = sinkCoordinates.X * 10; // 1 метр = 10 пикселей
            float sinkY = sinkCoordinates.Y * 10; // 1 метр = 10 пикселей

            // Убедимся, что мойка не выходит за границы кухни
            if (!room.Sink.IsCorner)
            {
                // Учитываем ширину мойки
                sinkX = Math.Max(offset, Math.Min(sinkX, room.Width * 10 - room.Sink.Width * 10 - offset));
                sinkY = Math.Max(offset, Math.Min(sinkY, room.Depth * 10 - room.Sink.Depth * 10 - offset));
            }
            else
            {
                sinkX = Math.Max(0, Math.Min(sinkX, room.Width * 10 - room.Sink.Width * 10));
                sinkY = Math.Max(0, Math.Min(sinkY, room.Depth * 10 - room.Sink.Depth * 10));
            }

            // Устанавливаем позицию умывальника
            room.Sink.Position = (sinkX / 10, sinkY / 10); // Сохраняем в метрах

            // Позиция трубы с водой внутри мойки
            room.Sink.PipePosition = (sinkX + (room.Sink.Width * 10 / 2), sinkY + (room.Sink.Depth * 10 / 2));

            // Размещение плиты рядом с мойкой
            float stoveX, stoveY;

            // Плита справа от умывальника
            if (sinkX + room.Sink.Width * 10 + offset <= room.Width * 10)
            {
                stoveX = sinkX + room.Sink.Width * 10 + offset;
                stoveY = sinkY;
            }
            // Плита сверху от умывальника
            else if (sinkY + room.Stove.Depth * 10 + offset <= room.Depth * 10)
            {
                stoveX = sinkX;
                stoveY = sinkY + room.Sink.Depth * 10 + offset;
            }
            else
            {
                // Если нет места, размещаем плиту в верхнем левом углу
                stoveX = 0;
                stoveY = 0;
            }

            room.Stove.Position = (stoveX / 10, stoveY / 10); // Сохраняем в метрах
        }


    }
}
