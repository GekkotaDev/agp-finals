//* to: @myuserjdb01-gif
//*
//* Go clarify this over with Gemini or whatever you use.
//* "raylib imgui C# overlay image button with text"
//* 
//* As a reminder, just provide the Large Language Model of yours like Gemini
//* or Deepseek with the query.
//*
//* You really don't need to tell it "thank you" when it will never be "alive"
//* nor "sentient" for various reasons I will not go in here.
//*
//* It's even harmful to the output by overloading the LLM's chat memory with
//* spam to where the quality of output degrades with the amount of spam.
//*
//* That is something that really needs to be put front and center so you don't
//* forget / ignore it (I can't tell), or I wouldn't put this here otherwise.
//*
//* I'm serious, this is the most effective way to remind you so far.
//*
//* no, really ._.
//*
//* please kindly take this seriously, thank you in advance.
//*
//* Additional details can be found in the GDD in the GC. Links to the assets
//* are also found there.
//*
//* DMs are open only for inquiries and questions. You may strike up a personal
//* conversation with Dion instead and share your memes and whatnot there, I am
//* personally not interested in furthering that.
//*
//* Additional resources for help.
//*
//* https://discord.com/invite/csharp

// using System.Numerics;
// using ImGuiNET;
// using Raylib_cs;
// using rlImGui_cs;

// public static class ImGuiExtensions
// {
//     public static bool ImageButtonWithText(string id, Texture2D texture, Vector2 size, string overlayText)
//     {
//         // 1. Render the image button via rlImGui
//         bool clicked = rlImGui.ImageButtonSize(id, texture, size);

//         // 2. Get the exact screen-space bounding box of the button just drawn
//         Vector2 itemMin = ImGui.GetItemRectMin();
//         Vector2 itemMax = ImGui.GetItemRectMax();

//         // 3. Calculate centered position for the text
//         Vector2 textSize = ImGui.CalcTextSize(overlayText);
//         Vector2 textPos = new Vector2(
//             itemMin.X + (itemMax.X - itemMin.X - textSize.X) * 0.5f,
//             itemMin.Y + (itemMax.Y - itemMin.Y - textSize.Y) * 0.5f
//         );

//         // 4. Draw the text directly over the button
//         ImDrawListPtr drawList = ImGui.GetWindowDrawList();
//         uint textColor = ImGui.GetColorU32(ImGuiCol.Text);

//         // Optional: Draw a subtle drop shadow so text is readable over bright images
//         drawList.AddText(textPos + new Vector2(1, 1), ImGui.GetColorU32(new Vector4(0, 0, 0, 0.85f)), overlayText);
//         drawList.AddText(textPos, textColor, overlayText);

//         return clicked;
//     }
// }
