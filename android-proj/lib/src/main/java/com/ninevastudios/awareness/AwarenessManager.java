package com.ninevastudios.awareness;

import android.app.Activity;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.support.annotation.Keep;
import android.text.TextUtils;
import android.util.Log;

import com.google.android.gms.awareness.fence.FenceState;

@Keep
public class AwarenessManager {

    private static final String TAG = AwarenessManager.class.getSimpleName();
    private static final String FENCE_RECEIVER_ACTION = BuildConfig.APPLICATION_ID + "FENCE_RECEIVER_ACTION";
    private static PendingIntent pendingIntent;

    @Keep
    public static void register(Activity activity) {
        activity.registerReceiver(new FenceReceiver(), new IntentFilter(FENCE_RECEIVER_ACTION));
    }

    @Keep
    public static PendingIntent getPendingIntent(Activity activity) {
        if (pendingIntent == null) {
            Intent intent = new Intent(FENCE_RECEIVER_ACTION);
            pendingIntent = PendingIntent.getBroadcast(activity, 0, intent, 0);
        }

        return pendingIntent;
    }

    @Keep
    public static class FenceReceiver extends BroadcastReceiver {

        @Override
        public void onReceive(Context context, Intent intent) {
            if (!TextUtils.equals(FENCE_RECEIVER_ACTION, intent.getAction())) {
                Log.d(TAG, "Received an unsupported action in FenceReceiver: action=" + intent.getAction());
                return;
            }

            FenceState fenceState = FenceState.extract(intent);
            Log.d(TAG, fenceState.toString());
        }
    }
}
