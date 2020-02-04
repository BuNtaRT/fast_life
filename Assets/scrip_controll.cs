using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;



public class scrip_controll : MonoBehaviour
{
    public bool che = true;
    int lsi = -1;                           //отдельный счетчик для листа с текстом
    public Button Main_Button;             // главная кнопка для отключения во время анимации
    public GameObject Text_template;        // обьект строка для записи текста

    public RectTransform Temp_Image;           //Обьект для вывода картинки 

    public GameObject[] image = new GameObject[5];  //все картинки 

    public RectTransform content_panel;     // позиция content в scroll

    string defaultway = "text/";

    //ВЫБОРЫ//ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ//
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //******************************************************************************************************************

    List<int> ACTION = new List<int>();

    //******************************************************************************************************************
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ////ВЫБОРЫ//


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////// выбор
    public GameObject choice;
    public GameObject v1, v2, v3, v4;
    public Text text_v1, text_v2, text_v3, text_v4;
    int choiceEND;
    public Text text_choice;
    bool lockrepeat = false;    //что бы не посторялись действия в call action (при выборах)
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public Slider count_Slider;
    public Slider Time_slider;
    public Image Background_Time_lsider, fill_Time_slider, Handle_time_slider;
    public Color default_color, disablet_color;

    public GameObject HideImage;            //картинка которая при активации закрывает другую тем самым очищая экран
    StreamReader neofread;                  // ст рид для неофицального имени 
    StreamReader read;                      // стрим рид для всего
    int moneyFamaly = 1;    //деньги семьи 
    int YVR = 4;            //уровень внимания со строны родителей
    public double road = 1.0;      //путь по сюжету 

    string MainString, TempS;      //сюда загружается текст из файла 
    //переменные с именами которые надо запомнить навсегда
    string UchitelName, MamaName, PapaName, DrugShool, UchLang, UchMatem, Director, Huliganshool, shoollove;
    string Name, NeOfName, famili, otchestvo; //для ввода случайносгенерированных переменых
    string LastName, LastNeOfName, LastFamil, LastOtch;   //последнии сгенер имена итд

    public float timePoi = 1;                   //время вывода текста
    public int countText = 1;                   //колличество выводимого текста текста

    public int colPoi = 1;                      //количество появляймого текста от 1-4
    public static bool destroyImage = false;   ///если вкл то уничтожение картинки 

    List<string> ls = new List<string>(); //листы с текстом где каждая строка новый элемент
    int temp_i_for_auto = 0;    //временная переменная чтобы переносить счетчик с touch in auto

    //////////////////setting
    public Text textInfoSec;
    public Text textInfoCont;

    //////////////////////////
    public Text text;


    int st;//кол-во выводимых строк (авто)
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        

        int cheI = PlayerPrefs.GetInt("cheI");
        float flcheck;
        flcheck = PlayerPrefs.GetFloat("TimePoi");
        if (flcheck != 0) {
            timePoi = PlayerPrefs.GetFloat("TimePoi");
        }
        timePoi = (float)System.Math.Round(timePoi, 3);
        Time_slider.value = timePoi;
        //////////////////////////////////////////////////////////////////
        che = true;
        textInfoSec.text = "Вывод каждые " + timePoi + " сек";
        Background_Time_lsider.color = default_color;
        fill_Time_slider.color = default_color;
        Handle_time_slider.color = default_color;

        if (cheI == 1) {
            che = false;
            Background_Time_lsider.color = disablet_color;
            fill_Time_slider.color = disablet_color;
            Handle_time_slider.color = disablet_color;

        }
        //////////////////////////////////////////////////////////////////
        int TempI = PlayerPrefs.GetInt("CountText");
        if (TempI != 0) {
            countText = PlayerPrefs.GetInt("CountText");
        }
        count_Slider.value = countText;
        textInfoCont.text = "количество выводимых строчек " + countText;



        PlayerPrefs.DeleteAll();


    }


    ////////////////////////////////////////////////////////////////////////////

    void combinput(float Tr) {      // упрощение взятия нужного текста для начала 
        var textFile = Resources.Load<TextAsset>(defaultway + Tr.ToString() + "." + moneyFamaly.ToString() + YVR.ToString());
        MainString = textFile.ToString();
        scantext();
    }
    ////////////////////////////////////////////////////////////////////////////


    void truecombit(float Tr)
    {
        Debug.Log("truecombit");
        var textFile = Resources.Load<TextAsset>(defaultway + Tr.ToString());
        MainString = textFile.ToString();
        scantext();
    }


    void choce(int v)
    {
        v1.SetActive(false);
        v2.SetActive(false);
        v3.SetActive(false);
        v4.SetActive(false);


        if (v >= 1) {
            var textFile = Resources.Load<TextAsset>(defaultway + "1");
            v1.SetActive(true);
            text_v1.text = textFile.ToString();
        }
        if (v >= 2)
        {
            var textFile = Resources.Load<TextAsset>(defaultway + "2");
            v2.SetActive(true);
            text_v2.text = textFile.ToString();
        }
        if (v >= 3)
        {
            var textFile = Resources.Load<TextAsset>(defaultway + "3");
            v3.SetActive(true);
            text_v3.text = textFile.ToString();
        }
        if (v >= 4)
        {
            var textFile = Resources.Load<TextAsset>(defaultway + "4");
            v4.SetActive(true);
            text_v4.text = textFile.ToString();
        }
        choiceEND = 0;
        choice.SetActive(true);
    }
    public void chouce1() { choiceEND = 1; }
    public void chouce2() { choiceEND = 2; }
    public void chouce3() { choiceEND = 3; }
    public void chouce4() { choiceEND = 4; }




    void pictireDrawn(GameObject pict) { //ставим картинку 
        destroyImage = false;
        GameObject temp = Instantiate(pict);
        temp.transform.SetParent(Temp_Image.transform, false);

    }
    ////////////////////////////////////////////////////////////////////////////
    void randomnexttime() {

        Random rand = new Random();
        int random = 0;
        random = Random.Range(1, 8);
        var textFile = Resources.Load<TextAsset>("text/next_time/" + random.ToString());
        MainString = textFile.ToString();
        scantext(); 
    }


    void choiceOptimiz(string defway,int cho,float r1, float r2,float r3,float r4,string defwayshou1, string defwayshou2, string defwayshou3, string defwayshou4) {
        // оптимизатор /+к основному пути
                                    //кол во выборов
                                            //какую дорогу поставить в случае выбора аврианта
                                                                                  //какой файл показать в случае выбора варианта 
        
        if (lockrepeat == false)
        {
            defaultway += defway;
            choce(cho);
            lockrepeat = true;
        }
        if (choiceEND != 0)
        {
            choice.SetActive(false);
            ACTION.Add(choiceEND);
            if (choiceEND >= 1)
            {
                road = r1;
                var textFile = Resources.Load<TextAsset>(defaultway + defwayshou1);
                MainString = textFile.ToString();
                scantext();
            }
            if (choiceEND >= 2)
            {
                road = r2;
                var textFile = Resources.Load<TextAsset>(defaultway + defwayshou2);
                MainString = textFile.ToString();
                scantext();
            }
            if (choiceEND >= 3)
            {
                road = r3;
                var textFile = Resources.Load<TextAsset>(defaultway + defwayshou3);
                MainString = textFile.ToString();
                scantext();
            }
            if (choiceEND >= 4)
            {
                road = r4;
                var textFile = Resources.Load<TextAsset>(defaultway + defwayshou4);
                MainString = textFile.ToString();
                scantext();
            }
            choiceEND = 0; lockrepeat = false;
        }
    }

    public void call_action()   
    {

        Debug.Log("call Action che ="+ che + " ct " + countText);
        if (che == false)       
        {
            if (st != 0) { Debug.Log("go spawn");Spawn_text();  }
        }
        else    //лок при автовыводе
        {
            Debug.Log("false else");

            Main_Button.enabled = false;
        }

        if (st == 0)
        {
            Debug.Log("in main if callact road =" + road);
            temp_i_for_auto = 0; 
            ls.Clear(); //чистим лист со строчками(а лучше места не нашел ??)
            if (road == 5.11f)
            {
                text_choice.text = "И все же, я решил...";
                choiceOptimiz("/1.1/",2,5.21f,5.22f,0,0,"1.1", "1.1", "","");
            }

            if (road == 4.5f)
            {
                text_choice.text = "Так как? Ты будешь?";
                choiceOptimiz("4.14/", 2, 5.11f, 5.21f, 0, 0, "1.1", "2", "", "");
            }
            if (road == 4.0f)
            {
                Debug.Log("road= " + road);
                pictireDrawn(image[1]);
                road = 4.5f;
                
                combinput(4.0f);
            }
            if (road == 3.5f)
            {
                road = 4f;
                randomnexttime();
                
            }
            if (road == 3.0f)
            {
                Debug.Log("road= " + road);
                pictireDrawn(HideImage);
                road = 3.5f;
                combinput(3.0f);
            }
            if (road == 2.1111f)
            {
                var textFile = Resources.Load<TextAsset>(defaultway + "2.1111");
                pictireDrawn(image[0]);//class
                MainString = textFile.ToString();
                road = 3.0f;
                scantext();
            }
            if (road == 2.0f)
            {
                road = 2.1111f;
                combinput(2.0f);
            }
            if (road == 1.1f)
            {
                string tmp="";
                Random rand = new Random();
                int r = 0;
                r = Random.Range(0, 2);
                if (r == 0) { tmp = "1.1111"; }
                if (r == 1) { tmp = "1.1112"; }
                if (r == 2) { tmp = "1.1113"; }
                var textFile = Resources.Load<TextAsset>(defaultway + tmp);
                MainString = textFile.ToString();
                road = 2.0f;
                scantext();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////road 1.0
            if (road == 1.0)
            {
                var textFile = Resources.Load<TextAsset>(defaultway + road.ToString() + "." + moneyFamaly.ToString() + YVR.ToString());
                MainString = textFile.ToString();
                road = 1.1f;
                scantext();
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////
    /// 
    /// 
    /// 
    /// 
    /// 
    ///////////////////////////////////////////////////////////////////////////////////////Сканирование текста на наличие специальных символов 


    void scantext() {
            Debug.Log("scantxt ");
        string allname = "";

        do {
            if (MainString.Contains("&1"))
            {
                int a = MainString.IndexOf("&1");
                char imia = MainString[a + 2];           //1- мужское 2-женское 3-муж неофицал 4- женск неофицал 0-не надо
                char famil = MainString[a + 3];          //1- мужская 2-женская 0-не надо  
                char ochest = MainString[a + 4];         //очество 1- мужское 2-женское 0-нет   
                MainString = MainString.Remove(a,5);
                /////////////////////
                if (imia == '0') {Name = "0"; NeOfName = "0"; }
                if (imia == '1'|| imia == '3') { im(imia); allname = NeOfName; }
                if (imia == '2' || imia == '4') { im(imia); allname = Name; }
                /////////////////////
                if (famil == '0') { famili = "0"; }
                else { familia(famil); allname += " " + famili; }
                /////////////////////
                if (ochest == '0') { otchestvo = "0"; }
                else { ochestvoFunc(ochest); allname += " " + otchestvo;  }
                /////////////////////
                LastName = Name;
                LastNeOfName = NeOfName;
                LastFamil = famili;
                LastOtch = otchestvo;
                MainString = MainString.Insert(a, allname);

            }
            //////////////////////////////////////////////////////////////////////////////////////&2
            if (MainString.Contains("&2"))
            {
                int a = MainString.IndexOf("&2");
                string all ="";
                char imia = MainString[a + 2];           
                char famil = MainString[a + 3];          
                char NeoffIM = MainString[a + 4];          
                char ochest = MainString[a + 5];         
                char specialName = MainString[a + 6];
                MainString = MainString.Remove(a, 7);
                if (specialName == '0') {
                    if (imia == '1') { all = LastName; }
                    if (NeoffIM == '1') { all += LastNeOfName; }
                    if (famil == '1') { all += LastFamil; }
                    if (ochest == '1') { all +=  LastOtch; }
                }
                else
                {
                    string TMain="";
                    int Chast = 0;
                    if (specialName == '1') { all = UchitelName; }
                    for (int i = 0; i < all.Length; i++) {
                        if (all[i] == ' ' || all[i] == ']') {
                            Chast++;
                            if (Chast == 1) { Name = TMain; }
                            if (Chast == 2) { famili = TMain; }
                            if (Chast == 3) { otchestvo = TMain; }
                            if (Chast == 4) { NeOfName = TMain; }
                            TMain = "";
                            all = all.Remove(i, 1);
                        }
                        TMain += all[i]; 
                    }
                    if (imia == '1') { all = Name; }
                    if (NeoffIM == '1') { all += NeOfName; }
                    if (famil == '1') { all += famili; }
                    if (ochest == '1') { all += otchestvo; }
                }
                MainString = MainString.Insert(a, allname);
            }
            //////////////////////////////////////////////////////////////////////////////////////&2 end
            //////////////////////////////////////////////////////////////////////////////////////&3


        } while (MainString.Contains("&"));
        read_text();
    }

   

    void im(char type) {
        Random rand = new Random();
        int r=0;
        string tempNeofString = "";
        string tempstring = "";
        if (type == '1'|| type == '3')      //генерация мужкого офф и не офф имени
        {
            r = Random.Range(0, 68);
            var textFile = Resources.Load<TextAsset>("NameMList");
            string[] arr = textFile.ToString().Split('\n');
            tempstring = arr[r];
            textFile = Resources.Load<TextAsset>("NameMListNeof");
            arr = textFile.ToString().Split('\n');
            tempNeofString = arr[r];
        }
        if (type == '2' || type == '4')     //генерация женского офф и не офф имени
        {
            r = Random.Range(0, 68);
            var textFile = Resources.Load<TextAsset>("NameFem");
            string[] arr = textFile.ToString().Split('\n');
            tempstring = arr[r];
            textFile = Resources.Load<TextAsset>("NameFemNEOF");
            arr = textFile.ToString().Split('\n');
            tempNeofString = arr[r];
        }
        Name = tempstring;
        NeOfName = tempNeofString;
    }



    void familia(char type)
    {
        Random rand = new Random();
        int r;
        r = Random.Range(0, 498);
        var textFile = Resources.Load<TextAsset>("FamilList");
        string[] arr = textFile.ToString().Split('\n');
        famili = arr[r];
        if (type=='2') {
            famili += 'а';
        }
    }
    void ochestvoFunc(char type)
    {
        Random rand = new Random();
        int r;
        r = Random.Range(0, 71);
        var textFile = Resources.Load<TextAsset>("otechList");
        string[] arr = textFile.ToString().Split('\n');
        otchestvo = arr[r];
        if (type=='2')
        {
            int dl = otchestvo.Length;
            dl=  dl-2;
            otchestvo = otchestvo.Remove(dl, 2);
            otchestvo += "на";
        }
    }
   
    /////////////////////////////////////////////////////////////////////////////////////// 



    ///////////////////////////////////////////////////////////////////////////////////////
    public void read_text()
    {
            Debug.Log("readtxt " + MainString);

        TempS = "";
        int SpaceCheck = 0;
        int it = -1, last_spaceIN_MAIN = 0;
        ///////////////////////////////////////////////
        for (int i = 0; i < MainString.Length; i++)
        {
      

            it++;
            if (MainString[i] == ' ')
            {                                                       // на случай если понадобится знать где последний пробел 
                SpaceCheck = it;
                last_spaceIN_MAIN = i;
            }
            TempS += MainString[i];

            if (it == 35 || MainString[i] == '$')                                           // режим строку на 35 символе или узнаем где последний пробел и режим там 
            {
                if (MainString[i] =='$') {
                    int dl = TempS.Length;
                    dl--;
                    TempS = TempS.Remove(dl, 1);
                }
                if (MainString[i+1] == ' ' || MainString[i] == ' ' || MainString[i] == '.' || MainString[i] == ',' || MainString[i] == ':' || MainString[i] == '!' || MainString[i] == '?' || MainString[i] == ';' || MainString[i] == '(' || MainString[i] == ')')
                {
                    if (TempS[0] == ' ')
                    {
                        TempS = TempS.Remove(0, 1);
                    }
                    it = -1;
                    SpaceCheck = 0;
                    ls.Add(TempS);
                    TempS = "";

                    st++;
                }
                else
                {
                    TempS = TempS.Remove(SpaceCheck, 36 - SpaceCheck);
                    i = last_spaceIN_MAIN;
                    SpaceCheck = 0;
                    it = -1;
                    if (TempS[0] == ' ') {
                        TempS = TempS.Remove(0, 1);
                    }
                    ls.Add(TempS);
                    
                    TempS = "";
                    st++;


                }
            }

        }
        if (TempS != "")
        {
            ls.Add(TempS);
            TempS = "";
            st++;
        }

        if (che == false)   //если вывод по нажатию
        {
            Debug.Log("che false go spawn che = " + che);

            Spawn_text();
        }
        else
        {
            Debug.Log("che true go rasp che = " + che + " ct " + countText);

            rasp();  //кидаем в вывод по времени  
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void rasp() {  // вывод по времени 
       StartCoroutine(Spawn_text(timePoi));
        Main_Button.enabled = true;
    }
    IEnumerator Spawn_text(float a )///////////////////////////////////////// расширяет контент и помещает туда текст 
    {
        Debug.Log("in spawn text " + st);

        int tempCT =0;

        int i = 0;
        if (temp_i_for_auto != 0) { i = temp_i_for_auto+1; temp_i_for_auto = 0; }
        for (; i < st; i++) {
            Debug.Log("in for " + st+" i="+i +" ct "+countText);
            Debug.Log(ls[i]);


            int tp = st - i;            //если оставшехся элементов меньше counttext тогда на  время приравниваем counttext значение tp
            if (tp < countText) {
                Debug.Log("in if" + " ct " + countText);

                tempCT = countText;
                countText = tp;
            }

            if (countText == 1)
            {
                Text_template.GetComponent<Text>().text = ls[i]; functext();
            }
            if (countText == 2)
            {
                Debug.Log("inct2 " + countText);

                Text_template.GetComponent<Text>().text = ls[i];functext();i++;
                Text_template.GetComponent<Text>().text = ls[i];functext();

            }
            if (countText == 3)
            {
                Text_template.GetComponent<Text>().text = ls[i]; functext(); i++;
                Text_template.GetComponent<Text>().text = ls[i]; functext(); i++;
                Text_template.GetComponent<Text>().text = ls[i]; functext();
            }
            if (countText == 4)
            {
                Text_template.GetComponent<Text>().text = ls[i]; functext(); i++;
                Text_template.GetComponent<Text>().text = ls[i]; functext(); i++;
                Text_template.GetComponent<Text>().text = ls[i]; functext(); i++;
                Text_template.GetComponent<Text>().text = ls[i]; functext();
            }
            if (countText == 5)
            {
                Text_template.GetComponent<Text>().text = ls[i]; functext(); i++;
                Text_template.GetComponent<Text>().text = ls[i]; functext(); i++;
                Text_template.GetComponent<Text>().text = ls[i]; functext(); i++;
                Text_template.GetComponent<Text>().text = ls[i]; functext(); i++;
                Text_template.GetComponent<Text>().text = ls[i]; functext();
            }
            yield return new WaitForSeconds(a);
            
        }

        if (tempCT!=0) { countText = tempCT; }
        st = 0; 
        Main_Button.enabled = true;
        Debug.Log("Mb enable");

    }
    void functext() {
        GameObject temp = Instantiate(Text_template);
        temp.transform.SetParent(content_panel.transform, false);
        content_panel.offsetMax += new Vector2(0, 85);
    }


    void Spawn_text()///////////////////////////////////////// вывод по нажатию
    {

           

        lsi = lsi + 1;
      

        temp_i_for_auto = lsi;
        if (lsi >= st)
        {
            Debug.Log("lsi>st lsi = " + lsi + " st = " + st);
            st = 0;
            lsi = -1;
            call_action();
        }
        else
        {
            if (countText == 1)
            {
                Text_template.GetComponent<Text>().text = ls[lsi]; functext();
            }
            if (countText == 2)
            {
                Text_template.GetComponent<Text>().text = ls[lsi]; functext(); lsi++;
                Text_template.GetComponent<Text>().text = ls[lsi]; functext();
            }
            if (countText == 3)
            {
                Text_template.GetComponent<Text>().text = ls[lsi]; functext(); lsi++;
                Text_template.GetComponent<Text>().text = ls[lsi]; functext(); lsi++;
                Text_template.GetComponent<Text>().text = ls[lsi]; functext();
            }
            if (countText == 4)
            {
                Text_template.GetComponent<Text>().text = ls[lsi]; functext(); lsi++;
                Text_template.GetComponent<Text>().text = ls[lsi]; functext(); lsi++;
                Text_template.GetComponent<Text>().text = ls[lsi]; functext(); lsi++;
                Text_template.GetComponent<Text>().text = ls[lsi]; functext();
            }
            if (countText == 5)
            {
                Text_template.GetComponent<Text>().text = ls[lsi]; functext(); lsi++;
                Text_template.GetComponent<Text>().text = ls[lsi]; functext(); lsi++;
                Text_template.GetComponent<Text>().text = ls[lsi]; functext(); lsi++;
                Text_template.GetComponent<Text>().text = ls[lsi]; functext(); lsi++;
                Text_template.GetComponent<Text>().text = ls[lsi]; functext();
            }
        }
    }



    ////////////////////////////////////////////////////////////SETTING/////////////////////////////////

    public void SliderValueChanged(float newValue) {
        timePoi = newValue;
        timePoi = (float)System.Math.Round(timePoi, 3);
        textInfoSec.text = "Вывод каждые "+timePoi+" сек";
        PlayerPrefs.SetFloat("TimePoi", timePoi);
        PlayerPrefs.Save();
    }
    public void Slidercountvalue(float val)
    {

        countText = (int)val;

        textInfoCont.text = "ко личество выводимых строчек "+ countText;
        PlayerPrefs.SetInt("CountText", countText);
        PlayerPrefs.Save();
    }




    public void checkButton(bool ch) {


        if (ch == true) {
            textInfoSec.text = "Вывод каждые " + timePoi + " сек";
            che = true;
            Time_slider.enabled = true;
            Background_Time_lsider.color = default_color;
            fill_Time_slider.color = default_color;
            Handle_time_slider.color = default_color;
            PlayerPrefs.SetInt("cheI", 2);
        }
        else {
            textInfoSec.text = "Вывод каждое нажатие";
            che = false;
            Time_slider.enabled = false;
            Background_Time_lsider.color = disablet_color;
            fill_Time_slider.color = disablet_color;
            Handle_time_slider.color = disablet_color;
            PlayerPrefs.SetInt("cheI",1);
        }
        PlayerPrefs.Save();
        che = ch;
        Debug.Log("touch che = " + che);
    }
}

