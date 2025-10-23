namespace lista10.ViewModels
{
    public class GameViewModel
    {

        private static int compare(int secret, int guess)
        {
            if (secret > guess)
            {
                return -1;
            }
            else if (secret == guess)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }
        public static (int, int) calculateWin((int, int, int, int) game_params)
        {
            (int range, int secret, int guess1, int guess2) = game_params;

            int res1 = compare(secret, guess1);
            int res2 = compare(secret, guess2);

            return (res1, res2);
        }
    }
}
