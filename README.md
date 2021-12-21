# NPCGener_Editor
Application to edit NPCGener following W2PP format.<br><br>
With this WPF Windows app it's possible to create, edit, delete, retrieve and visualize any information about NPCGener.

-----------------

### What is NPCGener?
- NPCGener is a file that W2's servers based on W2PP reads to know where, when, how long and much more information about control of any NPC.

-----------------

### How do I use?
- To begin, you'll need to have an `I_NPCGener.txt` file at the same directory of the app.
  - I'll let a example file of this one (`example_files/I_NPCGener.txt`). Follow the pattern and you should be safe.
- Every modification that you want to do on NPCGener, should be done by `NPCGener_Editor`.
- After every single modification (add, edit, delete, safe or any other), you MUST click `Gerar Index` button.
![image](https://user-images.githubusercontent.com/37567719/146969894-15887001-0b84-4be8-a9a9-b2aed2df80da.png)
- `Gerar Index` button will do the Index Control of your NPCGener.
  - With this, the final NPCGener (that one of W2PP reads) will be created on the previous path of the application.
- Follow this path hierarchy and you'll should be fine too:
  - `TMSrv/run/Create_NPCGener_Folder/This_App`
  - Thus, your final NPCGener will stay at the correct folder (`TMSrv/run/NPCGener.txt` / `TMSrv/run/NPCGener.new.txt`)
