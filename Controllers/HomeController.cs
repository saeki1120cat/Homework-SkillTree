using System.Collections.Generic;
using System.Diagnostics;
using Homework.Models;
using Homework_SkillTree.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework_SkillTree.Controllers
{
    public class HomeController : Controller
    {


        private static List<TestData> _transactions = new List<TestData>();
        private static int _currentId = 1; // �y�����_�l��


        [HttpGet]
        public IActionResult Index()
        {
            // ���ʹ��ո��
            var testData = GenerateTestData();
            // �p�G _transactions ���šA�~���ʹ��ո��
            if (!_transactions.Any())
            {
                _transactions.AddRange(testData);
                _currentId = _transactions.Max(t => t.Id) + 1;
            }

            // �N��ƶǻ��� View
            return View(_transactions); // �ǻ���ƨ� index.cshtml
        }



        [HttpPost]
        public IActionResult AddData(TestData model)
        {
            if (ModelState.IsValid)
            {
                model.Id = _currentId++; // �]�w�y����
                _transactions.Add(model); // �s�W��ƨ�C��
                return RedirectToAction("Index"); // ���s�ɦV�� Index ����
            }

            // �p�G���ҥ��ѡA��^ Index �����A����ܥثe�����
            return View("Index", _transactions);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<TestData> GenerateTestData()
        {
            var data = new List<TestData>();
            for (int i = 0; i < 10; i++)
            {
                data.Add(new TestData
                {
                    Id = i + 1, // �y����
                    Category = i % 2 == 0 ? "���J" : "��X", // ���Ƭ����J�A�_�Ƭ���X
                    Money = new Random().Next(1, 10000), // �H�����B�A�d�� 1 ~ 10000
                    Date = DateTime.Now.AddDays(-i), // ��������ѩ��e�� i ��
                    Description = $"�o�O�� {i + 1} �����ո��"
                });
            }
            return data;
        }
    }
}