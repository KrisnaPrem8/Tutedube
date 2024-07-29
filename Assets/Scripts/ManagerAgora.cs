using Agora.Rtc;
using UnityEngine;

public class ManagerAgora : MonoBehaviour
{
    public string appId = "YOUR_AGORA_APP_ID";
    public string token = "YOUR_TOKEN";
    public string channelName = "YOUR_CHANNEL_NAME";
    internal IRtcEngine RtcEngine;

    void Start()
    {
        SetupEngine();
        InitEventHandler();
    }

    private void SetupEngine()
    {
        // Create an IRtcEngine instance
        RtcEngine = Agora.Rtc.RtcEngine.CreateAgoraRtcEngine();
        RtcEngineContext context = new RtcEngineContext();
        context.appId = appId;
        context.channelProfile = CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING;
        context.audioScenario = AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT;
        // Initialize the instance
        RtcEngine.Initialize(context);
    }

    public void Join()
    {
        // Enable the video module
        RtcEngine.EnableVideo();
        // Set channel media options
        ChannelMediaOptions options = new ChannelMediaOptions();
        // Automatically subscribe to all audio streams
        options.autoSubscribeAudio.SetValue(true);
        // Automatically subscribe to all video streams
        options.autoSubscribeVideo.SetValue(true);
        // Set the channel profile to live broadcast
        options.channelProfile.SetValue(CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_COMMUNICATION);
        //Set the user role as host
        options.clientRoleType.SetValue(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);
        // Join a channel
        Debug.Log(RtcEngine.JoinChannel(token, channelName, 0, options));
    }


    public void Leave()
    {
        Debug.Log("Leaving _channelName");
        // Leave the channel
        RtcEngine.LeaveChannel();
        // Disable the video module
        RtcEngine.DisableVideo();
    }


    // Create a user event handler instance and set it as the engine event handler
    private void InitEventHandler()
    {
        UserEventHandler handler = new UserEventHandler(this);
        RtcEngine.InitEventHandler(handler);
    }

    // Implement your own EventHandler class by inheriting the IRtcEngineEventHandler interface class implementation
    internal class UserEventHandler : IRtcEngineEventHandler
    {
        private readonly ManagerAgora _videoSample;
        internal UserEventHandler(ManagerAgora videoSample)
        {
            _videoSample = videoSample;
        }
        // error callback
        public override void OnError(int err, string msg)
        {
        }
        // Triggered when a local user successfully joins the channel
        public override void OnJoinChannelSuccess(RtcConnection connection, int elapsed)
        {
            Debug.Log("Joined Channel Success");
        }
    }

    void OnApplicationQuit()
    {
        if (RtcEngine != null)
        {
            Leave();
            // Destroy IRtcEngine
            RtcEngine.Dispose();
            RtcEngine = null;
        }
    }
}
