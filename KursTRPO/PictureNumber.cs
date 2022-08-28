namespace KursTRPO
{
    class PictureNumber
    {
        int section;
        int number;
        public PictureNumber(int section1, int number1)
        {
            section = section1;
            number = number1;
        }
        public bool IsBefore(PictureNumber picNum) => 
            (picNum.section - section == 0 && picNum.number - number == 1) 
            || (picNum.section - section == 1 && picNum.number == 1);
        public override string ToString() => section == 0 ? 
            number.ToString() : section.ToString() + '.' + number.ToString();
    }
}
