using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> FemaleNames = new List<string>(){ "Kasia", "Anastazja", "Lina", "Dovile", "Spinach Sarah", "Peppermint Patty", "Margherita Maria", "Marta" };
    public List<string> MaleNames = new List<string>(){ "Garlic Greg", "Bacon Bob", "Hawaiian Hank", "Mikkel", "Abderrahman", "BBQ Billy", "Simonas", "David", "Kristinn" };
    public List<string> Professions = new List<string>(){ "accountant", "actor", "animator", "architect", "baker", "biologist", "builder", "butcher", "career counselor", "nursing home caretaker", "comic book writer", "ceo of a big company", "programmer", "chef", "decorator", "dentist", "designer","director", "doctor", "economist", "editor", "electrician", "engineer", "executive", "farmer", "film director", "fisherman", "flight attendant", "garbage collector", "geologist", "hairdresser", "head teacher", "jeweler", "journalist", "judge", "lawyer", "lecturer", "library assistant", "makeup artist", "manager", "miner", "musician", "nurse", "optician", "painter", "personal assistant", "photographer", "pilot", "plumber", "police officer", "politician", "porter", "printer", "prison officer", "professional gambler", "receptionist", "sailor", "stay at home parent", "salesperson", "scientist", "secretary", "shop assistant","sign language Interpreter", "singer", "soldier", "solicitor", "surgeon", "tailor", "teacher", "translator", "travel agent", "trucker", "TV cameraman", "TV presenter", "vet", "waiter", "web designer", "writer", "game developer" };
    public List<string> ChildActivities = new List<string>(){ "accountant for teddy bears", "actor in school play", "dollhouse architect", "sand cake baker", "little biologist", "lego builder", "spiderman fan", "teddybear doctor", "cartoon binge-watcher", "goldfish owner", "mommy's hairdresser", "teddybear teacher", "juggler", "daddy's assistant", "little wizard", "ukulele player", "nurse for dolls", "finger-painter", "frog photographer", "pretend police officer", "little chemist", "mommy's assistant", "nursery rhymes singer", "tinsoldier owner", "teddybear surgeon", "doll's tailor", "translator from imaginary language", "batman fan", "hamsters owner", "fairytales writer", "imaginary friend's caretaker", "video game player" };
    public List<string> AdultLastWords = new List<string>(){ "As a fungi lover, this pizza was a mushroom-tastic dream come true!", "This pizza was the wurst... Just kidding! It was actually the best pizza I've ever had!" ,"Some people say pineapple doesn't belong on pizza, but those people are wrong. This pizza was a tropical delight!", "This pizza was so cheesy, I felt like I was on a first-name basis with the dairy cow who produced the cheese!", "I'm a vegan, so I'm always looking for good meatless options. This pizza was so delicious, I almost forgot it was vegan!", "This pizza had so much ham and pineapple, I felt like I was on a tropical island vacation. Aloha, deliciousness!", "This pizza was so cool and refreshing, I felt like I was eating a pizza-shaped breath mint!", "This pizza had so much BBQ sauce, I started to wonder if I should invite it to my next backyard cookout.", "This pizza was so creamy and tangy, I almost started dipping my other foods in ranch dressing out of habit.", "This pizza had so many toppings, I felt like I was at an all-you-can-eat pizza buffet. And I was okay with that." };
    public List<string> ChildLastWords = new List<string>(){ "This pizza was so good, I almost shed a pepper-tear of joy!", "This pizza was so hot and spicy, I had to call the fire department... to order another one!", "This pizza was like a carnivore's dream come true. So many meats, so little time!", "This pizza was like a beautiful Italian sunset... Simple, yet breathtakingly beautiful!", "This pizza had so much buffalo sauce, I felt like I was in the wild west! Yee-haw!", "This pizza had so much garlic, I felt like a vampire-repellent factory!", "This pizza was so healthy, I almost felt like I was cheating on my diet. Almost.", "This pizza had so much bacon, I started to wonder if there was such a thing as too much bacon. But then I took another bite.", "This pizza had so many artichokes, I felt like I was eating a pizza-shaped garden. But a really tasty garden.", "This pizza had so much pesto, I felt like I was eating a pizza-shaped basil plant. But a really delicious basil plant." };

    public void Start()
    {
        ObjectiveManager.Instance.OnObjectiveCompleted.AddListener(showDeadCharacter);
    }
    public void showDeadCharacter() {
        Object[] RandomImages = Resources.LoadAll("Sprites/character_faces", typeof(Sprite));
        Sprite RandomImage = (Sprite)RandomImages[Random.Range(0, 68)];

        string CharacterName = "";
        string Age = "";
        string Occupation = "";
        string LastWords = "";

        if (RandomImage.name.Contains("f")) {
            CharacterName = FemaleNames[Random.Range(0, FemaleNames.Count - 1)];
        } else if (RandomImage.name.Contains("m")) {
            CharacterName = MaleNames[Random.Range(0, MaleNames.Count - 1)];
        }

        if (RandomImage.name.Contains("a")) {
            if (RandomImage.name.Contains("y")) {
                Age = Random.Range(21, 39).ToString();
            } else if (RandomImage.name.Contains("o")) {
                Age = Random.Range(40, 60).ToString();
            }
           Occupation = Professions[Random.Range(0, Professions.Count - 1)];
           LastWords = AdultLastWords[Random.Range(0, AdultLastWords.Count - 1)];
        } else if (RandomImage.name.Contains("c")) {
            if (RandomImage.name.Contains("y")) {
                Age = Random.Range(3, 5).ToString();
            } else if (RandomImage.name.Contains("o")) {
                Age = Random.Range(6, 11).ToString();
            }
           Occupation = ChildActivities[Random.Range(0, ChildActivities.Count - 1)];
           LastWords = ChildLastWords[Random.Range(0, ChildLastWords.Count - 1)];
        }

        gameObject.transform.Find("Bio").GetComponent<Text>().text = CharacterName + ", " + Age + ", " + Occupation;
        gameObject.transform.Find("LastWords").GetComponent<Text>().text = '"' + LastWords + '"';
        gameObject.transform.Find("Image").GetComponent<Image>().sprite = RandomImage;

        GetComponent<BioInfoDisplay>().Display();
    }
}
