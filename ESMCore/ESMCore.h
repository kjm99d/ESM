#pragma once

#ifdef ESMCORE_EXPORTS
#define ESMCORE_EXPORT extern "C" __declspec(dllexport)
#else
#define ESMCORE_EXPORT extern "C" __declspec(dllimport)
#endif

ESMCORE_EXPORT int ESMCore_Initalize();
ESMCORE_EXPORT int ESMCore_Finalize();
ESMCORE_EXPORT int ESMCore_AddSchedule();
