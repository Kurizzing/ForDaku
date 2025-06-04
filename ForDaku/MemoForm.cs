using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForDaku
{
    public partial class MemoForm : Form
    {
        private const string urlMM = "https://www.masterduelmeta.com/";
        private const string urlMD = "https://gall.dcinside.com/mgallery/board/lists/?id=masterduel";
        private const string urlYT = "https://www.youtube.com/@Kim_Daku";
        private const string urlCF = "https://cafe.naver.com/kimdaku";
        private const int margin = 20;

        private readonly Font richTextBoxFont = new Font("굴림체", 30);
        private readonly List<string> optionList = new List<string>() { "빈도순", "이름순", "무작위", "최대최소순" };

        private RouletteForm rouletteForm;
        private List<(string, int)> itemList = new List<(string, int)>();

        private TextBox searchTextBox;
        private Button searchButton;
        private Label label;
        private int lastSelectionLength = -1; // 마지막 선택 영역 길이
        private int lastSearchIndex = 0;

        private int i = 0;

        public MemoForm()
        {
            InitializeComponent();
            InitializeControls();
            RepositionControls();
            SetHotkeys();
            this.SizeChanged += MemoForm_SizeChanged;
        }

        private void SetHotkeys()
        {
            this.KeyPreview = true;
            this.KeyDown += (sender, e) =>
            {
                if (e.Control && e.KeyCode == Keys.F)
                {
                    // 'Ctrl + F' 눌렀을 때 찾기 대화상자 표시
                    ShowFindDialog(deckTextBox);
                }
                if (e.Control && e.KeyCode == Keys.S)
                {
                    SaveToFile();  // 기존에 만든 저장 함수 호출
                    e.SuppressKeyPress = true; // 삑 소리 방지
                }
                if (e.Control && e.KeyCode == Keys.R)
                {
                    e.Handled = true;           // 기본 동작(예: 새로고침 등) 막기

                    sortButton.PerformClick();  // 버튼 클릭 동작 실행
                    deckTextBox.SelectionStart = 0;  // 커서를 텍스트의 첫 번째로 설정
                    deckTextBox.ScrollToCaret();     // 스크롤을 커서 위치로 이동시킴
                }
            };
        }

        private void MemoForm_SizeChanged(object sender, EventArgs e)
        {
            RepositionControls();
        }

        private void RepositionControls()
        {
            if (this.ClientSize.Width < 1920)
            {
                // deckTextBox
                deckTextBox.Width = this.ClientSize.Width / 2;

                // memoLabel
                memoTitleLabel.Location = new Point(deckTextBox.Location.X + deckTextBox.Width + margin, margin);

                // memoTextBox
                memoTextBox.Width = this.ClientSize.Width / 2 - margin;
                memoTextBox.Height = this.ClientSize.Height / 2;
                memoTextBox.Location = new Point(deckTextBox.Location.X + deckTextBox.Width + margin, memoTitleLabel.Location.Y + memoTitleLabel.Height + margin);

                // extraPanel: saveLoadPanel, sortPanel, urlPanel
                extraPanel.Location = new Point(memoTextBox.Location.X, memoTextBox.Location.Y + memoTextBox.Height + margin);

                // makeRouletteButton
                makeRouletteButton.Location = new Point(memoTextBox.Location.X, this.ClientSize.Height - makeRouletteButton.Height - margin);
            }
            else
            {
                // deckTextBox
                deckTextBox.Width = this.ClientSize.Width / 3 + 100;

                // memoLabel
                memoTitleLabel.Location = new Point(deckTextBox.Location.X + deckTextBox.Width + margin, margin);

                // memoTextBox
                memoTextBox.Width = this.ClientSize.Width / 3 + 100;
                memoTextBox.Height = this.ClientSize.Height / 3;
                memoTextBox.Location = new Point(deckTextBox.Location.X + deckTextBox.Width + margin, memoTitleLabel.Location.Y + memoTitleLabel.Height + margin);

                // extraPanel: saveLoadPanel, sortPanel, urlPanel
                extraPanel.Location = new Point(memoTextBox.Location.X, memoTextBox.Location.Y + memoTextBox.Height + margin);

                // makeRouletteButton
                makeRouletteButton.Location = new Point(memoTextBox.Location.X, this.ClientSize.Height - makeRouletteButton.Height - margin);
            }
        }

        private void InitializeControls()
        {
            sortButton.FlatStyle = FlatStyle.Flat;
            sortButton.FlatAppearance.BorderSize = 0;
            SetResizedButtonImageWithAspect(sortButton, sortButton.Image);

            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.FlatAppearance.BorderSize = 0;
            SetResizedButtonImageWithAspect(saveButton, saveButton.Image);

            loadButton.FlatStyle = FlatStyle.Flat;
            loadButton.FlatAppearance.BorderSize = 0;
            SetResizedButtonImageWithAspect(loadButton, loadButton.Image);

            urlButtonMM.FlatStyle = FlatStyle.Flat;
            urlButtonMM.FlatAppearance.BorderSize = 0;
            SetResizedButtonImageWithAspect(urlButtonMM, urlButtonMM.Image);

            urlButtonDC.FlatStyle = FlatStyle.Flat;
            urlButtonDC.FlatAppearance.BorderSize = 0;
            SetResizedButtonImageWithAspect(urlButtonDC, urlButtonDC.Image);

            urlButtonYT.FlatStyle = FlatStyle.Flat;
            urlButtonYT.FlatAppearance.BorderSize = 0;
            SetResizedButtonImageWithAspect(urlButtonYT, urlButtonYT.Image);

            urlButtonCF.FlatStyle = FlatStyle.Flat;
            urlButtonCF.FlatAppearance.BorderSize = 0;
            SetResizedButtonImageWithAspect(urlButtonCF, urlButtonCF.Image);

            deckTextBox.Font = richTextBoxFont;
            deckTextBox.SelectionFont = richTextBoxFont;
            deckTextBox.ImeMode = ImeMode.Hangul;
            deckTextBox.LanguageOption = 0;         // 영어 폰트 사용 못하게 설정
            deckTextBox.Text = "";
            deckTextBox.GotFocus += DeckTextBoxFocused;

            memoTextBox.Font = richTextBoxFont;
            memoTextBox.SelectionFont = richTextBoxFont;
            memoTextBox.ImeMode = ImeMode.Hangul;
            memoTextBox.LanguageOption = 0;

            foreach (var option in optionList)
            {
                optionComboBox.Items.Add(option);
            }
            optionComboBox.SelectedIndex = 0; // 기본값 설정
        }

        private void SetResizedButtonImageWithAspect(Button btn, Image originalImage)
        {
            int buttonWidth = btn.ClientSize.Width;
            int buttonHeight = btn.ClientSize.Height;

            float imageAspect = (float)originalImage.Width / originalImage.Height;
            float buttonAspect = (float)buttonWidth / buttonHeight;

            int drawWidth, drawHeight;

            if (imageAspect > buttonAspect)
            {
                // 이미지가 버튼보다 더 가로로 긴 경우 → 가로 최대
                drawWidth = buttonWidth;
                drawHeight = (int)(buttonWidth / imageAspect);
            }
            else
            {
                // 이미지가 버튼보다 더 세로로 긴 경우 → 세로 최대
                drawHeight = buttonHeight;
                drawWidth = (int)(buttonHeight * imageAspect);
            }

            // 리사이징된 이미지 생성
            Bitmap resizedImage = new Bitmap(drawWidth, drawHeight);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.Clear(Color.Transparent);
                g.DrawImage(originalImage, 0, 0, drawWidth, drawHeight);
            }

            btn.Image = resizedImage;
            btn.ImageAlign = ContentAlignment.MiddleCenter;
        }

        private void DeckTextBoxFocused(object sender, EventArgs e)
        {
            RichTextBox richTextBox = deckTextBox;
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
            findForm.Text = "찾기";
            findForm.Size = new Size(300, 120);
            findForm.StartPosition = FormStartPosition.Manual;
            findForm.Location = new Point(this.Location.X + 800, this.Location.Y + 100); // 메모장 폼 위치에 따라 대화상자 위치 조정
            findForm.Owner = this; // 메모장 폼을 소유자로 설정
            // 폼 확대 축소 막기
            findForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            findForm.MaximizeBox = false;
            findForm.MinimizeBox = false;
            findForm.Activated += (s, e) =>
            {
                if (lastSearchIndex >= 0 && lastSelectionLength >= 0)
                {
                    // 대화상자에 포커스가 들어오면 강조 복원 
                    richTextBox.Select(lastSearchIndex, lastSelectionLength);
                    richTextBox.SelectionBackColor = Color.LightBlue;   // 강조 색상 설정
                }
            };
            searchTextBox = new TextBox { Dock = DockStyle.Top };
            searchButton = new Button { Text = "찾기", Dock = DockStyle.Bottom };

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
                lastSelectionLength = -1;
                lastSearchIndex = 0;
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
                    richTextBox.Select(lastSearchIndex, lastSelectionLength);   // 이전 선택 영역 복원
                    richTextBox.SelectionBackColor = richTextBox.BackColor;     // 배경색 복원
                }

                richTextBox.Select(foundIndex, searchText.Length);  // 텍스트를 찾으면 해당 부분을 선택(강조)
                richTextBox.ScrollToCaret();                        // 스크롤을 선택된 텍스트로 이동
                richTextBox.SelectionBackColor = Color.LightBlue;   // 강조 색상 설정

                lastSearchIndex = foundIndex;                       // 마지막 검색 위치 갱신
                lastSelectionLength = searchText.Length;            // 마지막 선택 영역 길이 갱신
            }
            else
            {
                // 텍스트를 못 찾은 경우에는 메시지 표시
                MessageBox.Show("해당 텍스트가 존재하지 않습니다!");
            }
        }

        private List<(string, int)> MakeList()
        {
            var itemList = new List<(string, int)>();

            string textBoxString = deckTextBox.Text;
            string[] lines = textBoxString.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue; // 빈 줄은 무시

                string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                {
                    throw new FormatException($"입력 형식이 잘못되었습니다: (라인: '{line}')");
                }

                string deckName = parts[0];
                int count;

                try
                {
                    count = int.Parse(parts[1]);
                }
                catch (FormatException)
                {
                    throw new FormatException($"숫자 형식이 잘못되었습니다: '{parts[1]}' (라인: '{line}')");
                }

                itemList.Add((deckName, count));
            }

            return itemList;
        }

        private void makeRouletteButton_Click(object sender, EventArgs e)
        {
            List<(string, int)> listTemp;
            try
            {
                listTemp = MakeList();
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"입력 오류: {ex.Message}", "오류");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"알 수 없는 오류: {ex.Message}", "오류");
                return;
            }
            itemList = listTemp;

            rouletteForm = new RouletteForm(this, itemList);

            rouletteForm.Show();
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = deckTextBox;

            List<(string, int)> listTemp;
            try
            {
                listTemp = MakeList();
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"입력 오류: {ex.Message}", "오류");
                return; // 여기서 이후 코드 실행을 중지
            }
            catch (Exception ex)
            {
                MessageBox.Show($"알 수 없는 오류: {ex.Message}", "오류");
                return;
            }
            itemList = listTemp;

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
                    for (int i = 0; i < descending.Count || i < ascending.Count; i++)
                    {
                        if (i < descending.Count)
                            result.Add(descending[i]);
                        if (i < ascending.Count)
                            result.Add(ascending[i]);
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

        private void SaveToFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.Title = "Save Text File";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, deckTextBox.Text);
            }
        }

        private void LoadFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.Title = "Open Text File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                deckTextBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveToFile();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            LoadFromFile();
        }

        private void OpenURL(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true   // 필수: 기본 브라우저로 열리게 함
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("웹 페이지를 열 수 없습니다: " + ex.Message);
            }
        }

        private void UrlButtonMM_Click(object sender, EventArgs e)
        {
            OpenURL(urlMM);
        }

        private void UrlButtonDC_Click(object sender, EventArgs e)
        {
            OpenURL(urlMD);
        }

        private void UrlButtonYT_Click(object sender, EventArgs e)
        {
            OpenURL(urlYT);
        }

        private void UrlButtonCF_Click(object sender, EventArgs e)
        {
            OpenURL(urlCF);
        }
    }
}
