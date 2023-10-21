namespace JaguarGame.Models.EventsArgs;

public class RoundEndedEventArgs : EventArgs
{
    public required int round { get; init; }
    public BoardEvaluationData boardEvaluation { get; init; }
    public PlaceRef jagPoss { get; init; }
    public int dogsAliveCount { get; init; }
    public struct BoardEvaluationData
    {
        public float jaguarChangeToWin { get; init; }
        public float dogsChangeToWin { get; init; }
    }
}