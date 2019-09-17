
(function () {
    $(function () {
        loadGrid();
    })
}());

//装载表格
function loadGrid() {
    $("#grid").jqGrid({
        height: 400,
        rownumbers: true,
        colModel: [
            {
                label: '标题',
                name: 'title',
                index: 'title',
                sortable: false,
            },
            {
                label: '账号',
                name: 'account',
                index: 'account',
                sortable: false,
            },
            {
                label: '密码',
                name: 'password',
                index: 'password',
                hidedlg: false,
                sortable: false,
            },
            {
                label: '地址',
                name: 'url',
                index: 'url',
                sortable: false,
            },
            {
                label: '程序名',
                name: 'program_name',
                index: 'program_name',
                sortable: false,
            },
            {
                label: ' ',
                name: 'select',
                align: 'center',
                sortable: false,
                formatter: function (value, options, row) {
                    var btn = "";
                    btn += '&nbsp;<a href="javascript:void(0)" onclick=onDtl(' + options.rowId + ') class="mesq">查看信息</a>&nbsp;';
                    btn += '&nbsp;<a href="javascript:void(0)" onclick=onDel(' + options.rowId + ') class="mesd">删除信息</a>&nbsp;';
                    return btn;
                }
            }
        ],
        shrinkToFit: true,
        rowNum: 20,
        rowList: [20, 50, 100],
        pager: '#pager',
        sortname: 'id',
        sortorder: "desc",
    });


}
//查看信息
function onDtl(id) {
    var data = $("#grid").jqGrid('getRowData', id);
    layer.alert("程序名：" + data.program_name + "</br>账号：" + data.account + "<br>密码：" + data.password + "<br>标题：" + data.title + "<br>地址：" + data.url + "<br>");
}
//删除
function onDel(id) {
    layer.confirm('您确定要删除吗？', {
        btn: ['确定', '取消']
    },
        function () {
            layer.alert("此处为删除信息的方法");
        }
    );
}
//查询
function onQuery() {

}

var password = null;

//显示输入密码
function shownAnswerSecret() {
    //prompt层
    layer.prompt({ title: '请输入密码', formType: 1 }, function (pass, index) {
        layer.close(index);
        password = pass;
        if (password !== null) {
            showAll(password);
        }
    });
}

//显示数据库数据
function showAll(password) {
    var data = cefCustomObject.returnData(password);
    try {
        var rows = JSON.parse(data);
        for (var i = 0; i < rows.length; i++) {
            $("#grid").jqGrid('addRowData', i + 1, rows[i]);
        }
    } catch (exception) {
        layer.alert("无法打开文件");
    }
}


//显示控制台调试信息
function showDevTools() {
    cefCustomObject.showDevTools();
}

//添加信息
function onAdd(form) {
    var temp = [];
    if (form.title.value) {
        var data = { "account": form.account.value, "password": form.password.value, "title": form.title.value, "url": form.url.value, "program_name": form.program_name.value };



        showAll(password);
    }
   
}

//显示添加信息的窗口
function showAddLayer() {
    var tableContent = " ";
    tableContent += ' <div>';
    tableContent += '   <form name="myForm" onsubmit="return onAdd(this)">';
    tableContent += '       <label for="title">标题：</label>          <input type="text" name="title" value=""><br>';
    tableContent += '       <label for="account">账号：</label>        <input type="text" name="account"><br>';
    tableContent += '       <label for="password">密码：</label>       <input type="text" name="password"><br>';
    tableContent += '       <label for="url">地址：</label>            <input type="text" name="url"><br>';
    tableContent += '       <label for="program_name">程序名：</label> <input type="text" name="program_name"><br>';
    tableContent += '       <input type="submit" value="提交">';
    tableContent += '   </form>';
    tableContent += ' </div>';

    layer.open({
        type: 1,
        skin: 'layui-layer-demo', //样式类名
        closeBtn: 1, //不显示关闭按钮
        anim: 2,
        shadeClose: true, //开启遮罩关闭
        content: tableContent
    });
}

//窗口变化时自适应宽度
window.onresize = function () {
    $("#grid").setGridWidth(document.body.clientWidth * 0.83);
}
