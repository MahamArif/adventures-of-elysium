//This script will make your token disappear when you touch it
function OnTriggerEnter (info : Collider) {
   // int scorepoint = 5;
    //ScoreUpdate scr = GameObject.Find("Score").GetComponent<ScoreUpdate>();
    //scr.Score(scorepoints);
	Destroy(gameObject);
}