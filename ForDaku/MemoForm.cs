using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForDaku
{
    public partial class MemoForm : Form
    {
        private TextBox searchTextBox;
        private Button searchButton;
        private int lastSelectionLength = -1; // 마지막 선택 영역 길이
        private int lastSearchIndex = 0;

        private Panel resizablePanel;
        private bool isResizing = false;
        private Point lastMousePosition;


        private RouletteForm rouletteForm;
        public MemoForm()
        {
            InitializeComponent();

            // RichTextBox 설정
            RichTextBox richTextBox = richTextBox1;
            richTextBox.Font = new Font("굴림", 30);
            richTextBox.SelectionFont = new Font("굴림", 30);
            richTextBox.ImeMode = ImeMode.Hangul;
            richTextBox.LanguageOption = 0;
            richTextBox.Text = "드래곤메이드 5";

            richTextBox1.GotFocus += RichTextBox_Focus;

            richTextBox2.Font = new Font("굴림", 30);
            richTextBox2.SelectionFont = new Font("굴림", 30);
            richTextBox2.ImeMode = ImeMode.Hangul;
            richTextBox2.LanguageOption = 0;

            //richTextBox1.LostFocus += (sender, e) =>
            //{
            //    label1.Text = "리치텍스트박스 포커스 아웃";
            //};

            // Ctrl + F 핫키 처리
            this.KeyPreview = true;
            this.KeyDown += (sender, e) =>
            {
                if (e.Control && e.KeyCode == Keys.F)
                {
                    // 'Ctrl + F' 눌렀을 때 찾기 대화상자 표시
                    ShowFindDialog(richTextBox);
                }
            };

            ComboBox sortOption = optionComboBox;
            sortOption.Items.Add("빈도순");
            sortOption.Items.Add("이름순");
            sortOption.Items.Add("무작위");
            sortOption.Items.Add("최대최소순");
            sortOption.SelectedIndex = 0; // 기본값 설정
            //sortOption.SelectedIndexChanged += (sender, e) => {
            //    label1.Text = "선택된 항목: " + sortOption.SelectedItem.ToString();
            //    // 선택된 항목에 따른 정렬 처리
            //};

            //// 폼이 비활성화되면 선택 영역 상태 저장
            //this.Deactivate += (sender, e) =>
            //{
            //    lastSelectionStart = richTextBox.SelectionStart;
            //    lastSelectionLength = richTextBox.SelectionLength;
            //};

            //// 폼이 활성화되면 선택 영역 복원
            //this.Activated += (sender, e) =>
            //{
            //    if (lastSelectionStart >= 0 && lastSelectionLength >= 0)
            //    {
            //        // 선택 영역 복원
            //        richTextBox.Select(lastSelectionStart, lastSelectionLength);
            //    }
            //};


            //// Panel 생성
            //resizablePanel = new Panel
            //{
            //    BorderStyle = BorderStyle.FixedSingle,
            //    BackColor = Color.LightGray,
            //    Location = new Point(50, 50),
            //    Size = new Size(200, 150)
            //};

            //// 패널에 마우스 이벤트 추가
            //resizablePanel.MouseDown += ResizablePanel_MouseDown;
            //resizablePanel.MouseMove += ResizablePanel_MouseMove;
            //resizablePanel.MouseUp += ResizablePanel_MouseUp;

            //// 폼에 패널 추가
            //this.Controls.Add(resizablePanel);
        }

        //// 마우스 버튼을 누르면 크기 조정을 시작합니다.
        //private void ResizablePanel_MouseDown(object sender, MouseEventArgs e)
        //{
        //    // 패널의 오른쪽 아래 모서리에서 클릭하면 크기 조정 시작
        //    if (e.X >= resizablePanel.Width - 10 && e.Y >= resizablePanel.Height - 10)
        //    {
        //        isResizing = true;
        //        lastMousePosition = e.Location;
        //        this.Cursor = Cursors.SizeNWSE; // 크기 조정 커서로 변경
        //    }
        //}

        //// 마우스를 이동시킬 때마다 크기를 조정합니다.
        //private void ResizablePanel_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (isResizing)
        //    {
        //        // 크기 조정
        //        int deltaX = e.X - lastMousePosition.X;
        //        int deltaY = e.Y - lastMousePosition.Y;

        //        // 패널의 크기 조정
        //        resizablePanel.Width += deltaX;
        //        resizablePanel.Height += deltaY;

        //        // 마지막 마우스 위치 갱신
        //        lastMousePosition = e.Location;
        //    }
        //    else
        //    {
        //        // 크기 조정 모드가 아닐 때는 기본 커서
        //        if (e.X >= resizablePanel.Width - 10 && e.Y >= resizablePanel.Height - 10)
        //            this.Cursor = Cursors.SizeNWSE; // 크기 조정 모양 커서
        //        else
        //            this.Cursor = Cursors.Default;  // 기본 커서
        //    }
        //}

        //// 마우스 버튼을 떼면 크기 조정을 종료합니다.
        //private void ResizablePanel_MouseUp(object sender, MouseEventArgs e)
        //{
        //    isResizing = false;
        //    this.Cursor = Cursors.Default;  // 크기 조정 완료 후 기본 커서로 돌아감
        //}

        private void RichTextBox_Focus(object sender, EventArgs e)
        {
            RichTextBox richTextBox = richTextBox1;
            // RichTextBox에 포커스가 들어오면 선택 영역 복원
            if (lastSearchIndex >= 0 && lastSelectionLength >= 0)
            {
                richTextBox.Select(lastSearchIndex, lastSelectionLength);
                richTextBox.SelectionBackColor = richTextBox.BackColor; // 배경색 복원
            }
        }

        private void ShowFindDialog(RichTextBox richTextBox)
        {
            Form findForm = new Form();
            findForm.Text = "Find";
            findForm.Size = new Size(300, 120);
            findForm.StartPosition = FormStartPosition.Manual;
            findForm.Location = new Point(this.Location.X + 800, this.Location.Y + 100); // 메모장 폼 위치에 따라 대화상자 위치 조정

            searchTextBox = new TextBox { Dock = DockStyle.Top };
            searchButton = new Button { Text = "Find", Dock = DockStyle.Bottom };

            findForm.Controls.Add(searchTextBox);
            findForm.Controls.Add(searchButton);

            // 버튼 클릭 이벤트: Find 버튼을 눌러서 텍스트를 찾을 때
            searchButton.Click += (sender, e) => FindText(richTextBox);

            // Enter 키로도 검색 가능하도록 설정
            searchTextBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // 삑 소리 방지
                    FindText(richTextBox);     // 엔터키를 눌렀을 때 텍스트 찾기
                }
            };

            findForm.FormClosing += (s, e) =>
            {
                // 폼 닫을 때 선택 영역 복원
                if (lastSearchIndex >= 0 && lastSelectionLength >= 0)
                {
                    richTextBox.Select(lastSearchIndex, lastSelectionLength);
                    richTextBox.SelectionBackColor = richTextBox.BackColor; // 배경색 복원
                }
            };

            // 엔터키 기본 동작을 Find 버튼으로 연결 (버튼 눌림 효과)
            findForm.AcceptButton = searchButton;

            findForm.Show();
        }

        private void FindText(RichTextBox richTextBox)
        {
            string searchText = searchTextBox.Text;
            if (string.IsNullOrEmpty(searchText)) return;

            // 찾기 시작할 위치 (마지막 검색 위치 이후로 시작)
            int startIndex = richTextBox.SelectionStart + richTextBox.SelectionLength;

            // 현재 위치 이후부터 검색
            int foundIndex = richTextBox.Find(searchText, startIndex, RichTextBoxFinds.None);

            if (foundIndex == -1) // 못 찾으면 처음부터 다시 검색
            {
                foundIndex = richTextBox.Find(searchText, 0, RichTextBoxFinds.None);
            }

            if (foundIndex >= 0)
            {
                if (lastSearchIndex >= 0 && lastSelectionLength >= 0)
                {
                    // 이전 선택 영역 복원
                    richTextBox.Select(lastSearchIndex, lastSelectionLength);
                    richTextBox.SelectionBackColor = richTextBox.BackColor; // 배경색 복원
                }
                // 텍스트를 찾으면 해당 부분을 선택(강조)
                richTextBox.Select(foundIndex, searchText.Length);
                richTextBox.ScrollToCaret(); // 스크롤을 선택된 텍스트로 이동
                richTextBox.SelectionBackColor = Color.LightBlue; // 강조 색상 설정


                // 강조된 텍스트가 잘 보이도록 포커스를 searchTextBox로 다시 돌려줍니다.
                //searchTextBox.Focus();   // 포커스를 searchTextBox로 되돌림

                // 추가된 부분: 텍스트 강조 후, RichTextBox가 포커스를 잃지 않도록 하여 강조 상태 유지
                //richTextBox.Focus();

                lastSearchIndex = foundIndex; // 마지막 검색 위치 갱신
                lastSelectionLength = searchText.Length; // 마지막 선택 영역 길이 갱신
            }
            else
            {
                // 텍스트를 못 찾은 경우에는 메시지 표시
                MessageBox.Show("Text not found!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rouletteForm = new RouletteForm(this);
            
            rouletteForm.Show();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MemoForm_Load(object sender, EventArgs e)
        {

        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            label1.Text = "정렬 버튼 클릭됨";
            RichTextBox richTextBox = richTextBox1;

            string textBoxString = richTextBox.Text;
            List<(string, int)> itemList = new List<(string, int)>();

            string[] lines = textBoxString.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
            label1.Text = $"{lines.Length}";


            foreach (var line in lines)
            {
                string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.None);

                if (parts.Length == 2)
                {
                    string deckName = parts[0];
                    int count = int.Parse(parts[1]);

                    itemList.Add((deckName, count));
                }
            }

            // 선택된 정렬 옵션에 따라 정렬
            switch (optionComboBox.SelectedIndex)
            {
                case 0: // 빈도순
                    itemList = itemList.OrderByDescending(x => x.Item2).ToList();
                    break;
                case 1: // 이름순
                    itemList = itemList.OrderBy(x => x.Item1).ToList();
                    break;
                case 2: // 무작위
                    Random random = new Random();
                    itemList = itemList.OrderBy(x => random.Next()).ToList();
                    break;
                case 3: // 최대최소순
                    var descending = itemList.OrderByDescending(x => x.Item2).ToList(); // 많은 값부터
                    var ascending = itemList.OrderBy(x => x.Item2).ToList();             // 적은 값부터

                    var result = new List<(string, int)>();
                    int i = 0;
                    while (i < descending.Count || i < ascending.Count)
                    {
                        if (i < descending.Count)
                            result.Add(descending[i]);
                        if (i < ascending.Count)
                            result.Add(ascending[i]);
                        i++;
                    }

                    // 중복 제거 (많은 값 == 적은 값인 항목이 중복될 수 있음)
                    result = result.Distinct().ToList();

                    itemList = result;
                    break;
            }

            // 정렬된 결과를 RichTextBox에 다시 표시
            richTextBox.Clear();
            foreach (var item in itemList)
            {
                richTextBox.AppendText($"{item.Item1} {item.Item2}{Environment.NewLine}");
            }
        }

        private void optionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
